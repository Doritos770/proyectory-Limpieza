using Microsoft.EntityFrameworkCore;
using System.Net;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Exceptions;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {

        private readonly IDapperContext _dapper;
        public PedidoRepository(VentasLimpiezaContext context, IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }



        public async Task<VentasLimpieza.Core.Auxiliares.resumen_venta> GetResumenGeneralVentas()
        {
            var sql = _dapper.Provider switch
            {
                DataBaseProvider.MySql => VentasLimpieza.Infrastructure.Queries.sqlEstadistica.ResumenGeneral,
                _ => throw new NotSupportedException("Provider no soportado")
            };

            var result = await _dapper.QueryAsync<VentasLimpieza.Core.Auxiliares.resumen_venta>(sql);
            return result.FirstOrDefault() ?? new VentasLimpieza.Core.Auxiliares.resumen_venta { TotalPedidos = 0, TotalIngresos = 0 };
        }
       
    }
}
