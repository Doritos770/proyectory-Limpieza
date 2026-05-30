using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Services.Services
{
    public class DetallepedidoService : IDetallepedidoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetallepedidoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Detallepedido>> GetAllDetallepedidosAsync(DetallepedidoQueryFilter? filters = null)
        {
            var detalles = await _unitOfWork.DetallepedidoRepository.GetAll();

            if (filters != null)
            {
                if (filters.Id.HasValue)
                {
                    detalles = detalles.Where(x => x.Id == filters.Id.Value);
                }
                if (filters.PedidoId.HasValue)
                {
                    detalles = detalles.Where(x => x.PedidoId == filters.PedidoId.Value);
                }
                if (filters.ProductoId.HasValue)
                {
                    detalles = detalles.Where(x => x.ProductoId == filters.ProductoId.Value);
                }
                if (filters.LoteProductoId.HasValue)
                {
                    detalles = detalles.Where(x => x.LoteProductoId == filters.LoteProductoId.Value);
                }
                if (filters.Cantidad.HasValue)
                {
                    detalles = detalles.Where(x => x.Cantidad == filters.Cantidad.Value);
                }
                if (filters.PrecioUnitarioMin.HasValue)
                {
                    detalles = detalles.Where(x => x.PrecioUnitario >= filters.PrecioUnitarioMin.Value);
                }
                if (filters.PrecioUnitarioMax.HasValue)
                {
                    detalles = detalles.Where(x => x.PrecioUnitario <= filters.PrecioUnitarioMax.Value);
                }
                if (filters.SubtotalMin.HasValue)
                {
                    detalles = detalles.Where(x => x.Subtotal >= filters.SubtotalMin.Value);
                }
                if (filters.SubtotalMax.HasValue)
                {
                    detalles = detalles.Where(x => x.Subtotal <= filters.SubtotalMax.Value);
                }
            }

            return detalles;
        }

        public async Task<Detallepedido> GetDetallepedidoByIdAsync(int id)
        {
            return await _unitOfWork.DetallepedidoRepository.GetById(id);
        }

        public async Task RegistrarDetallepedido(Detallepedido detallepedido)
        {
            await _unitOfWork.DetallepedidoRepository.Add(detallepedido);
            await _unitOfWork.SaveChangesAsync();
        }

        public void UpdateDetallepedido(Detallepedido detallepedido)
        {
            _unitOfWork.DetallepedidoRepository.Update(detallepedido);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteDetallepedido(int id)
        {
            await _unitOfWork.DetallepedidoRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}