using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Core.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task InsertUsuario(Categoria categoria);
        Task UpdateUsuario(Categoria categoria);
        Task DeleteUsuario(Categoria categoria);
    }
}
