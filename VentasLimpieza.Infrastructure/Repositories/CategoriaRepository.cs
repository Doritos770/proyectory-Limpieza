using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class CategoriaRepository// : ICategoriaRepository
    {
        public readonly VentasLimpiezaContext _categoria;

        public CategoriaRepository(VentasLimpiezaContext categoria)
        {
            _categoria = categoria;
        }


        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            var categorias = await _categoria.Categoria.ToListAsync();
            return categorias;
        }
        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            var categoria = await _categoria.Categoria.FirstOrDefaultAsync(x => x.Id == id);
            return categoria;
        }

        public async Task InsertUsuario(Categoria categoria)
        {
            _categoria.Categoria.Add(categoria);
            await _categoria.SaveChangesAsync();
        }

        public async Task UpdateUsuario(Categoria categoria)
        {
            _categoria.Categoria.Update(categoria);
            await _categoria.SaveChangesAsync();
        }
        public async Task DeleteUsuario(Categoria categoria)
        {
            _categoria.Categoria.Remove(categoria);
            await _categoria.SaveChangesAsync();
        }
    }
}
