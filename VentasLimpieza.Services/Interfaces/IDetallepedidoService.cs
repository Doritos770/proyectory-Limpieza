using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.QueryFilter;

namespace VentasLimpieza.Services.Interfaces
{
    public interface IDetallepedidoService
    {
        Task<IEnumerable<Detallepedido>> GetAllDetallepedidosAsync(DetallepedidoQueryFilter? filters = null);
        Task<Detallepedido> GetDetallepedidoByIdAsync(int id);
        Task RegistrarDetallepedido(Detallepedido detallepedido);
        void UpdateDetallepedido(Detallepedido detallepedido);
        Task DeleteDetallepedido(int id);
    }
}