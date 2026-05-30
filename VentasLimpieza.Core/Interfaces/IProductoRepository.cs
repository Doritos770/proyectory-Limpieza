using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Core.Interfaces
{
    public interface IProductoRepository : IBaseRepository<Producto>
    {
        Task<IEnumerable<Producto>> GetProductosPorLote();
        Task<IEnumerable<Producto>> GetProductosSinVenta();
        Task<IEnumerable<Producto>> GetEstadisticaProductoPorCategoria();
        Task<IEnumerable<Producto>> GetProductosConStockAgotado();
        Task<IEnumerable<VentasLimpieza.Core.Auxiliares.producto_mas_vendido>> GetProductosMasVendidos(int limit);
        Task<IEnumerable<VentasLimpieza.Core.Auxiliares.ganancia_lote>> GetGananciasPorLote();
    }
}