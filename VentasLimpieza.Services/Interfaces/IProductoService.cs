using Microsoft.Extensions.Hosting;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Core.Auxiliares;

namespace VentasLimpieza.Services.Interfaces
{
    public interface IProductoService
    {
        Task<ResponseData> GetAllProductsAsync(ProductoQueryFilter? filters = null);
        //Task<IEnumerable<Producto>> GetAllProductsAsync(ProductoQueryFilter? filters = null);
        Task<Producto> GetProductoByIdAsync(int id);
        Task RegistrarProducto(Producto producto);
        //void UpdateProducto(Producto producto);

        Task UpdateProducto(Producto producto);
        Task DeleteProducto(int id);
        /*************************************************/
        Task<IEnumerable<ProductoPorLote>> GetProductosPorLote();
        Task<IEnumerable<Producto>> GetProductosSinVenta();
        Task<IEnumerable<Producto>> GetEstadisticaProductoPorCategoria();
        Task<IEnumerable<Producto>> GetProductosConStockAgotado();
        Task<IEnumerable<producto_mas_vendido>> GetProductosMasVendidos(int limit);
        Task<IEnumerable<ganancia_lote>> GetGananciasPorLote();
    }
}
