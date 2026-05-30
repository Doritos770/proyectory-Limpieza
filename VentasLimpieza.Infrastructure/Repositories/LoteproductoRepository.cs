using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;
using VentasLimpieza.Infrastructure.Queries;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class LoteproductoRepository : BaseRepository<Loteproducto>, ILoteproductoRepository
    {
        private readonly IDapperContext _dapper;

        public LoteproductoRepository(VentasLimpiezaContext context, IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }


        public async Task<IEnumerable<LoteproductoSimple>> GetLotesCaducados()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => sqlLoteproducto.LotesCaducados,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<LoteproductoSimple>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<LoteproductoSimple>> GetLotesBajoStock(int stockMinimo)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => sqlLoteproducto.LotesBajoStock,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<LoteproductoSimple>(sql,new { StockMinimo = stockMinimo });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<LoteproductoSimple>> GetLotesProximosACaducar(int diasAntes)
        {
            var fechaActual = DateOnly.FromDateTime(DateTime.Now);
            var fechaLimite = DateOnly.FromDateTime(DateTime.Now.AddDays(diasAntes));
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => sqlLoteproducto.LotesProximosACaducar,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<LoteproductoSimple>(sql, new { FechaActual = fechaActual, FechaLimite = fechaLimite });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task ActualizarEstadoLote(Loteproducto lote)
            {
            lote.Activo = 0;
            Update(lote);
            }

        public async Task ReducirStock(int loteId, int cantidad)
        {
            var lote = await GetById(loteId);
            lote.CantidadDisponible -= cantidad;
            Update(lote);
        }

        public async Task<IEnumerable<Loteproducto>> GetLotesActivosPorProducto(int productoId)
        {
            return await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync(
                System.Linq.Queryable.OrderBy(
                    System.Linq.Queryable.Where(_entities, l => l.ProductoId == productoId && l.Activo == 1 && l.CantidadDisponible > 0), 
                    l => l.FechaCaducidad)
            );
        }

    }
}

