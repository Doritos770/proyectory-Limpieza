using VentasLimpieza.core.Entities;

namespace VentasLimpieza.Core.Interfaces
{
    public interface IReseñaRepository
    {
        Task<IEnumerable<Resena>> GetReseñasAsync();
        Task<Resena> GetReseñaByIdAsync(int id);
        Task InsertReseña(Resena direccion);
        Task UpdateReseña(Resena direccion);
        Task DeleteReseña(Resena direccion);
    }
}
