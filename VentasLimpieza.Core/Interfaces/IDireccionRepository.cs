using VentasLimpieza.core.Entities;

namespace VentasLimpieza.Core.Interfaces
{
    public interface IDireccionRepository
    {
        Task<IEnumerable<Direccion>> GetDireccionesAsync();
        Task<Direccion> GetDireccionByIdAsync(int id);
        Task InsertDireccion(Direccion direccion);
        Task UpdateDireccion(Direccion direccion);
        Task DeleteDireccion(Direccion direccion);
    }
}