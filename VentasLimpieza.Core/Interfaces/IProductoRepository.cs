using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Core.Interfaces
{
    public interface IProductoRepository : IBaseRepository<Producto>
    {
        Task<IEnumerable<ProductoPorLote>> GetProductosPorLote();
        Task<IEnumerable<Producto>> GetProductosSinVenta();
        Task<IEnumerable<Producto>> GetEstadisticaProductoPorCategoria();
        Task<IEnumerable<Producto>> GetProductosConStockAgotado();
        Task<IEnumerable<producto_mas_vendido>> GetProductosMasVendidos(int limit);
        Task<IEnumerable<ganancia_lote>> GetGananciasPorLote();
    }
}