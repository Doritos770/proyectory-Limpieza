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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly IDapperContext _dapper;

        public UsuarioRepository(VentasLimpiezaContext context,
            IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<ResenaSimple>> GetUsuariosResenas(int Id)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => slqUsuario.UsuarioResena,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<ResenaSimple>(sql, new { UsuarioId = Id });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosInactivos()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => slqUsuario.UsuariosInactivos,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Usuario>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UsuarioPedidosSimple>> GetUsuariosConMasPedidos()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => slqUsuario.UsuariosConMasPedidos,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<UsuarioPedidosSimple>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VentasLimpieza.Core.Auxiliares.pedido_usuario> GetEstadisticasPedidos(int usuarioId)
        {
            var sqlResumen = _dapper.Provider switch
            {
                DataBaseProvider.MySql => VentasLimpieza.Infrastructure.Queries.sqlEstadistica.PedidosPorUsuario,
                _ => throw new NotSupportedException("Provider no soportado")
            };

            var sqlLista = _dapper.Provider switch
            {
                DataBaseProvider.MySql => VentasLimpieza.Infrastructure.Queries.sqlEstadistica.PedidosListaPorUsuario,
                _ => throw new NotSupportedException("Provider no soportado")
            };

            var result = await _dapper.QueryAsync<VentasLimpieza.Core.Auxiliares.pedido_usuario>(sqlResumen, new { UsuarioId = usuarioId });
            var resumen = result.FirstOrDefault() ?? new VentasLimpieza.Core.Auxiliares.pedido_usuario { CantidadPedidos = 0, TotalGastado = 0 };

            var listaResult = await _dapper.QueryAsync<VentasLimpieza.Core.Auxiliares.pedido_simple_usuario>(sqlLista, new { UsuarioId = usuarioId });
            resumen.Pedidos = listaResult.ToList();

            return resumen;
        }

    }
}
