using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Exceptions;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Services.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PedidoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PedidoDto> CrearPedidoAsync(CrearPedidoDto pedidoDto)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetById(pedidoDto.UsuarioId);
            if (usuario == null)
            {
                throw new BussinesExeption("El usuario especificado no existe.", HttpStatusCode.NotFound);
            }

            if (pedidoDto.Detalles == null || !pedidoDto.Detalles.Any())
            {
                throw new BussinesExeption("El pedido debe contener al menos un producto.", HttpStatusCode.BadRequest);
            }

            if (pedidoDto.CostoEnvio < 0)
            {
                throw new BussinesExeption("El costo de envío no puede ser negativo.", HttpStatusCode.BadRequest);
            }

            var pedido = new Pedido
            {
                UsuarioId = pedidoDto.UsuarioId,
                FechaPedido = DateTime.Now,
                Estado = "Pendiente",
                CostoEnvio = pedidoDto.CostoEnvio,
                MetodoPago = pedidoDto.MetodoPago,
                Total = pedidoDto.CostoEnvio,
                Detallepedidos = new List<Detallepedido>()
            };

            foreach (var detalleDto in pedidoDto.Detalles)
            {
                var producto = await _unitOfWork.ProductoRepository.GetById(detalleDto.ProductoId);
                if (producto == null)
                {
                    throw new BussinesExeption($"El producto con ID {detalleDto.ProductoId} no existe.", HttpStatusCode.NotFound);
                }

                if (detalleDto.Cantidad <= 0)
                {
                    throw new BussinesExeption($"La cantidad para el producto {producto.Nombre} debe ser mayor a cero.", HttpStatusCode.BadRequest);
                }

                var lotesActivos = await _unitOfWork.LoteproductoRepository.GetLotesActivosPorProducto(detalleDto.ProductoId);
                var totalDisponible = lotesActivos.Sum(l => l.CantidadDisponible);

                if (totalDisponible < detalleDto.Cantidad)
                {
                    throw new BussinesExeption($"Stock insuficiente para el producto {producto.Nombre}. Disponible: {totalDisponible}, Solicitado: {detalleDto.Cantidad}", HttpStatusCode.BadRequest);
                }

                int cantidadRestante = detalleDto.Cantidad;

                foreach (var lote in lotesActivos)
                {
                    if (cantidadRestante <= 0) break;

                    int cantidadADescontar = Math.Min(cantidadRestante, lote.CantidadDisponible);
                    
                    lote.CantidadDisponible -= cantidadADescontar;
                    _unitOfWork.LoteproductoRepository.Update(lote);

                    decimal subtotal = lote.PrecioCompra * cantidadADescontar; // Idealmente el producto debería tener un PrecioVenta. Usaremos PrecioCompra como base o buscar en la entidad.
                    // Si Producto tiene precio, usamos ese. Vamos a suponer que usamos PrecioCompra por el momento.
                    // Verificamos si existe PrecioUnitario en Producto. La entidad Producto no tiene el campo Precio, el precio se maneja en el lote en este modelo aparentemente.

                    var detallePedido = new Detallepedido
                    {
                        ProductoId = detalleDto.ProductoId,
                        LoteProductoId = lote.Id,
                        Cantidad = cantidadADescontar,
                        PrecioUnitario = lote.PrecioCompra, 
                        Subtotal = subtotal
                    };

                    pedido.Detallepedidos.Add(detallePedido);
                    pedido.Total += subtotal;
                    cantidadRestante -= cantidadADescontar;
                }
            }

            await _unitOfWork.PedidoRepository.Add(pedido);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PedidoDto>(pedido);
        }

        public async Task<IEnumerable<PedidoDto>> ObtenerPedidosAsync()
        {
            var pedidos = await _unitOfWork.PedidoRepository.GetAll();
            return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
        }

        public async Task<PedidoDto> ObtenerPedidoPorIdAsync(int id)
        {
            var pedido = await _unitOfWork.PedidoRepository.GetById(id);
            if (pedido == null)
            {
                throw new BussinesExeption("El pedido no existe.", HttpStatusCode.NotFound);
            }
            return _mapper.Map<PedidoDto>(pedido);
        }

        public async Task ActualizarEstadoPedidoAsync(int pedidoId, string estado)
        {
            var pedido = await _unitOfWork.PedidoRepository.GetById(pedidoId);
            if (pedido == null)
            {
                throw new BussinesExeption("El pedido no existe.", HttpStatusCode.NotFound);
            }

            // Regla de negocio: Si ya fue entregado, no se puede modificar a nada más.
            if (pedido.Estado.Equals("Entregado", StringComparison.OrdinalIgnoreCase))
            {
                throw new BussinesExeption("No se puede modificar un pedido que ya fue entregado.", HttpStatusCode.BadRequest);
            }

            // Regla de negocio: Si ya está cancelado, no se puede reactivar.
            if (pedido.Estado.Equals("Cancelado", StringComparison.OrdinalIgnoreCase))
            {
                throw new BussinesExeption("El pedido ya fue cancelado y no puede reactivarse.", HttpStatusCode.BadRequest);
            }

            // Regla de negocio: Si se cancela, devolvemos el stock a los lotes.
            if (estado.Equals("Cancelado", StringComparison.OrdinalIgnoreCase) && 
                !pedido.Estado.Equals("Cancelado", StringComparison.OrdinalIgnoreCase))
            {
                var todosDetalles = await _unitOfWork.DetallepedidoRepository.GetAll();
                var detalles = todosDetalles.Where(d => d.PedidoId == pedidoId).ToList();

                foreach (var detalle in detalles)
                {
                    var lote = await _unitOfWork.LoteproductoRepository.GetById(detalle.LoteProductoId);
                    if (lote != null)
                    {
                        lote.CantidadDisponible += detalle.Cantidad;
                        _unitOfWork.LoteproductoRepository.Update(lote);
                    }
                }
            }

            pedido.Estado = estado;
            _unitOfWork.PedidoRepository.Update(pedido);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<resumen_venta> GetResumenGeneralVentasAsync()
        {
            return await _unitOfWork.PedidoRepository.GetResumenGeneralVentas();
        }
    }
}
