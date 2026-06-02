using Microsoft.EntityFrameworkCore;
using VentasLimpieza.core.Interfaces;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;
using VentasLimpieza.Infrastructure.Queries;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        //public readonly IDapperContext _dapper;

        //public ProductoRepository(VentasLimpiezaContext context) 
        //    : base(context)
        //{
        //  //  _productos = productos;
        //}
        private readonly IDapperContext _dapper;

        public ProductoRepository(VentasLimpiezaContext context,
            IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }
        /*
         
         */
        //public async Task<IEnumerable<Producto>> GetProductosPorLote()
        //{
        //    try
        //    {
        //        var sql = _dapper.Provider switch
        //        {
        //            DataBaseProvider.MySql => GetProductosPorLote.MySql,
        //            _ => throw new NotSupportedException("Provider no soportado")
        //        };

        //        return await _dapper.QueryAsync<Producto>(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        public async Task<IEnumerable<ProductoPorLote>> GetProductosPorLote()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => sqlProducto.reportePorLote,  
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<ProductoPorLote>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Producto>> GetProductosSinVenta()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => sqlProducto.productosSinVentas,  
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Producto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Producto>> GetEstadisticaProductoPorCategoria()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => sqlProducto.estadisticasPorCategoria,  
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Producto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Producto>> GetProductosConStockAgotado()
        {
            var productos = await GetAll();
            var productosAgotados = productos.Where(p => p.Loteproductos.Sum(l => l.CantidadDisponible) == 0);
            return productosAgotados;
        }

        public async Task<IEnumerable<producto_mas_vendido>> GetProductosMasVendidos(int limit)
        {
            var sql = _dapper.Provider switch
            {
                DataBaseProvider.MySql => sqlEstadistica.ProductosMasVendidos,
                _ => throw new NotSupportedException("Provider no soportado")
            };

            return await _dapper.QueryAsync<producto_mas_vendido>(sql, new { Limit = limit });
        }
        public async Task<IEnumerable<ganancia_lote>> GetGananciasPorLote()
        {
            var sql = _dapper.Provider switch
            {
                DataBaseProvider.MySql => sqlEstadistica.GananciasPorLote,
                _ => throw new NotSupportedException("Provider no soportado")
            };

            return await _dapper.QueryAsync<ganancia_lote>(sql);
        }
    }
}
