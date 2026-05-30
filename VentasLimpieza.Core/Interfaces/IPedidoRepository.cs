using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Core.Interfaces;

public interface IPedidoRepository : IBaseRepository<Pedido>
{
    Task<VentasLimpieza.Core.Auxiliares.resumen_venta> GetResumenGeneralVentas();
}