using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Interfaces;

namespace VentasLimpieza.core.Interfaces
{
    public interface ICodigoseguridadRepository : IBaseRepository<Codigoseguridad>
    {
        Task<string> GetUltimoCodigoByUsuarioIdAsync(int usuarioId);
        Task<string> GenerarCodigoSeguridad(int usuarioId);
    }
        
}
