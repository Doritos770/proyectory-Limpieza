
using Microsoft.EntityFrameworkCore;
using VentasLimpieza.core.Interfaces;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;
using VentasLimpieza.Infrastructure.Queries;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class CodigoseguridadRepository : BaseRepository<Codigoseguridad>, ICodigoseguridadRepository
    {
        private readonly IDapperContext _dapper;
        public CodigoseguridadRepository(VentasLimpiezaContext context,
            IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }


        public async Task<string> GetUltimoCodigoByUsuarioIdAsync(int usuarioId)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DataBaseProvider.MySql => slqCodigoseguridad.UltimoCodigo,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryFirstOrDefaultAsync<string>(sql, new { UsuarioId = usuarioId });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<string> GenerarCodigoSeguridad(int usuarioId)
        {
            var codigo = new Codigoseguridad
            {
                UsuarioId = usuarioId,
                Codigo = GenerarCodigoSeguridadGenerarCodigoAleatorio(),
                FechaCreacion = DateTime.Now,
                FechaExpiracion = DateTime.Now.AddMinutes(12),
                Usado = 0
            };

            await _entities.AddAsync(codigo);
            
            return codigo.Codigo;
        }
        private string GenerarCodigoSeguridadGenerarCodigoAleatorio()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}