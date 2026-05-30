using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Net;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Core.Helpers;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Services.Services
{
    public class LoteproductoService : ILoteproductoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoteproductoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<IEnumerable<Loteproducto>> GetAllLoteproductosAsync(LoteproductoQueryFilter? filters = null)
        public async Task<ResponseData> GetAllLoteproductosAsync(
            LoteproductoQueryFilter? filters = null)
        {
            var lotes = await _unitOfWork.LoteproductoRepository.GetAll();

            if (filters != null)
            {
                if (filters.Id.HasValue)
                {
                    lotes = lotes.Where(x => x.Id == filters.Id.Value);
                }
                if (filters.ProductoId.HasValue)
                {
                    lotes = lotes.Where(x => x.ProductoId == filters.ProductoId.Value);
                }
                if (!string.IsNullOrEmpty(filters.NumeroLote))
                {
                    lotes = lotes.Where(x => x.NumeroLote.ToLower().Contains(filters.NumeroLote.ToLower()));
                }
                if (!string.IsNullOrEmpty(filters.FechaFabricacion))
                {
                    string fechaAux = Procesos.ParseFechaFlexible(filters.FechaFabricacion);
                    if (fechaAux != null)
                    {
                        DateTime fechaFiltro = Convert.ToDateTime(fechaAux);
                        lotes = lotes.Where(x => x.FechaFabricacion.ToDateTime(TimeOnly.MinValue).Date == fechaFiltro.Date);
                    }
                }
                if (!string.IsNullOrEmpty(filters.FechaCaducidad))
                {
                    string fechaAux = Procesos.ParseFechaFlexible(filters.FechaCaducidad);
                    if (fechaAux != null)
                    {
                        DateTime fechaFiltro = Convert.ToDateTime(fechaAux);
                        lotes = lotes.Where(x => x.FechaCaducidad.ToDateTime(TimeOnly.MinValue).Date == fechaFiltro.Date);
                    }
                }
                if (filters.CantidadIngresoMin.HasValue)
                {
                    lotes = lotes.Where(x => x.CantidadIngreso >= filters.CantidadIngresoMin.Value);
                }
                if (filters.CantidadIngresoMax.HasValue)
                {
                    lotes = lotes.Where(x => x.CantidadIngreso <= filters.CantidadIngresoMax.Value);
                }
                if (filters.CantidadDisponibleMin.HasValue)
                {
                    lotes = lotes.Where(x => x.CantidadDisponible >= filters.CantidadDisponibleMin.Value);
                }
                if (filters.CantidadDisponibleMax.HasValue)
                {
                    lotes = lotes.Where(x => x.CantidadDisponible <= filters.CantidadDisponibleMax.Value);
                }
                if (filters.PrecioCompraMin.HasValue)
                {
                    lotes = lotes.Where(x => x.PrecioCompra >= filters.PrecioCompraMin.Value);
                }
                if (filters.PrecioCompraMax.HasValue)
                {
                    lotes = lotes.Where(x => x.PrecioCompra <= filters.PrecioCompraMax.Value);
                }
                if (!string.IsNullOrEmpty(filters.UbicacionAlmacen))
                {
                    lotes = lotes.Where(x => x.UbicacionAlmacen != null && x.UbicacionAlmacen.ToLower().Contains(filters.UbicacionAlmacen.ToLower()));
                }
                if (filters.Activo.HasValue)
                {
                    lotes = lotes.Where(x => x.Activo == filters.Activo.Value);
                }
            }
            var pagedProductos = PagedList<object>
                .Create(lotes, filters.PageNumber, filters.PageSize);

            if (pagedProductos.Any())
            {
                return new ResponseData()
                {
                    Messages = new Message[] { new() { Type = TypeMessage.success.ToString(),
                        Description = "Registros de posts recuperados correctamente" } },
                    Pagination = pagedProductos,
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new ResponseData()
                {
                    Messages = new Message[] { new() { Type = TypeMessage.warning.ToString(), Description = "No fue posible recuperar la cantidad de registros" } },
                    Pagination = pagedProductos,
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            //return lotes;
        }

        public async Task<Loteproducto> GetLoteproductoByIdAsync(int id)
        {
            return await _unitOfWork.LoteproductoRepository.GetById(id);
        }


        public async void ActualizarStock(int stockMin)
        {
            await DarBajaLotesCaducadosAsync();
            await MarcarLotesBajoStockAsync(stockMin);
        }


        public async Task<int> DarBajaLotesCaducadosAsync()
        {
            var lotes = await _unitOfWork.LoteproductoRepository.GetAll();
            var fechaActual = DateOnly.FromDateTime(DateTime.Now);

            var lotesCaducados = lotes.Where(l => l.FechaCaducidad < fechaActual && l.Activo == 1).ToList();

            foreach (var lote in lotesCaducados)
            {
                lote.Activo = 0;
                _unitOfWork.LoteproductoRepository.Update(lote);
            }

            if (lotesCaducados.Any())
            {
                await _unitOfWork.SaveChangesAsync();
            }

            return lotesCaducados.Count;
        }

        public async Task<int> MarcarLotesBajoStockAsync(int stockMinimo)
        {
            var lotes = await _unitOfWork.LoteproductoRepository.GetAll();

            var lotesBajoStock = lotes.Where(l => l.CantidadDisponible <= stockMinimo && l.Activo == 1).ToList();

            foreach (var lote in lotesBajoStock)
            {
                lote.Activo = 0; // O podrías usar otro campo de estado si existe
                _unitOfWork.LoteproductoRepository.Update(lote);
            }

            if (lotesBajoStock.Any())
            {
                await _unitOfWork.SaveChangesAsync();
            }

            return lotesBajoStock.Count;
        }

        public async Task<IEnumerable<LoteproductoSimple>> GetLotesCaducadosAsync()
        {
            return await _unitOfWork.LoteproductoRepository.GetLotesCaducados();
        }

        public async Task<IEnumerable<LoteproductoSimple>> GetLotesBajoStockAsync(int stockMinimo)
        {
            return await _unitOfWork.LoteproductoRepository.GetLotesBajoStock(stockMinimo);
        }

        public async Task<IEnumerable<LoteproductoSimple>> GetLotesProximosACaducarAsync(int diasAntes)
        {
            return await _unitOfWork.LoteproductoRepository.GetLotesProximosACaducar(diasAntes);
        }


        public async Task RegistrarLoteproducto(Loteproducto loteproducto)
        {
            // 1. Validar que el Producto existe
            var producto = await _unitOfWork.ProductoRepository.GetById(loteproducto.ProductoId);
            if (producto == null)
                throw new ArgumentException("El producto especificado no existe");

            // 2. Validar que el número de lote no esté duplicado
            var lotesExistentes = await _unitOfWork.LoteproductoRepository.GetAll();
            if (lotesExistentes.Any(l => l.NumeroLote == loteproducto.NumeroLote))
                throw new ArgumentException($"El número de lote {loteproducto.NumeroLote} ya existe");

            // 3. Validar fechas lógicas
            if (loteproducto.FechaCaducidad <= loteproducto.FechaFabricacion)
                throw new ArgumentException("La fecha de caducidad debe ser posterior a la fecha de fabricación");

            if (loteproducto.FechaFabricacion > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("La fecha de fabricación no puede ser futura");

            // 4. Validar cantidades positivas
            if (loteproducto.CantidadIngreso <= 0)
                throw new ArgumentException("La cantidad de ingreso debe ser mayor a 0");

            // 5. Validar precio de compra positivo
            if (loteproducto.PrecioCompra <= 0)
                throw new ArgumentException("El precio de compra debe ser mayor a 0");

            // 6. Establecer valores por defecto
            loteproducto.CantidadDisponible = loteproducto.CantidadIngreso;
            loteproducto.Activo = 1; // Activo por defecto

            await _unitOfWork.LoteproductoRepository.Add(loteproducto);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void UpdateLoteproducto(Loteproducto loteproducto)
        {
            // 1. Validar que el lote existe
            var loteExistente = await _unitOfWork.LoteproductoRepository.GetById(loteproducto.Id);
            if (loteExistente == null)
                throw new ArgumentException($"El lote con ID {loteproducto.Id} no existe");

            // 2. Validar que no se pueda modificar un lote inactivo
            if (loteExistente.Activo == 0)
                throw new InvalidOperationException("No se puede modificar un lote inactivo");

            // 3. Validar que el producto existe si se cambia
            if (loteproducto.ProductoId != loteExistente.ProductoId)
            {
                var producto = await _unitOfWork.ProductoRepository.GetById(loteproducto.ProductoId);
                if (producto == null)
                    throw new ArgumentException("El nuevo producto no existe");
            }

            // 4. Validar fechas si se modifican
            if (loteproducto.FechaCaducidad <= loteproducto.FechaFabricacion)
                throw new ArgumentException("La fecha de caducidad debe ser posterior a la de fabricación");

            // 5. Si se modifica la cantidad de ingreso, actualizar la disponible proporcionalmente
            if (loteproducto.CantidadIngreso != loteExistente.CantidadIngreso)
            {
                var diferencia = loteproducto.CantidadIngreso - loteExistente.CantidadIngreso;
                loteproducto.CantidadDisponible = loteExistente.CantidadDisponible + diferencia;

                if (loteproducto.CantidadDisponible < 0)
                    throw new ArgumentException("La cantidad disponible no puede ser negativa");
            }

            // 6. Validar que la cantidad disponible no supere la cantidad de ingreso
            if (loteproducto.CantidadDisponible > loteproducto.CantidadIngreso)
                throw new ArgumentException("La cantidad disponible no puede ser mayor a la cantidad de ingreso");

            _unitOfWork.LoteproductoRepository.Update(loteproducto);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteLoteproducto(int id)
        {
            await _unitOfWork.LoteproductoRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}