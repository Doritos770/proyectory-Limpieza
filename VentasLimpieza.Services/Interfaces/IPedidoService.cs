using System.Collections.Generic;
using System.Threading.Tasks;
using VentasLimpieza.core.Dtos;

namespace VentasLimpieza.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoDto> CrearPedidoAsync(CrearPedidoDto pedidoDto);
        Task<IEnumerable<PedidoDto>> ObtenerPedidosAsync();
        Task<PedidoDto> ObtenerPedidoPorIdAsync(int id);
        Task ActualizarEstadoPedidoAsync(int pedidoId, string estado);
        Task<VentasLimpieza.Core.Auxiliares.resumen_venta> GetResumenGeneralVentasAsync();
    }
}
