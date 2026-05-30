using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.QueryFilter;

namespace VentasLimpieza.Services.Interfaces
{
    public interface ICodigoseguridadService
    {
        Task<IEnumerable<Codigoseguridad>> GetAllCodigosAsync(CodigoseguridadQueryFilter? filters = null);
        Task<Codigoseguridad> GetCodigoByIdAsync(int id);
        Task<bool> VerificarCodigoPorUsuario(int usuarioId, string codigoSeg);
        Task<string> Solicitud_Codigo(int usuarioId);
      //  Task RegistrarCodigo(Codigoseguridad codigo);
      //  void UpdateCodigo(Codigoseguridad codigo);
      //  Task DeleteCodigo(int id);
    }
}
