using System.Collections.Generic;
using System.Threading.Tasks;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Auxiliares;

namespace VentasLimpieza.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoDto> CrearPedidoAsync(CrearPedidoDto pedidoDto);
        Task<IEnumerable<PedidoDto>> ObtenerPedidosAsync();
        Task<PedidoDto> ObtenerPedidoPorIdAsync(int id);
        Task ActualizarEstadoPedidoAsync(int pedidoId, string estado);
        Task<resumen_venta> GetResumenGeneralVentasAsync();
    }
}
