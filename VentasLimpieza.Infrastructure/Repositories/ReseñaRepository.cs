using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class ReseñaRepository //: IReseñaRepository
    {
        public readonly VentasLimpiezaContext _reseña;

        public ReseñaRepository(VentasLimpiezaContext reseña)
        {
            _reseña = reseña;
        }
        public async Task<IEnumerable<Resena>> GetReseñasAsync()
        {
            var reseñas = await _reseña.Resenas.ToListAsync();
            return reseñas;
        }
        public async Task<Resena> GetReseñaByIdAsync(int id)
        {
            var reseña = await _reseña.Resenas.FirstOrDefaultAsync(x => x.Id == id);
            return reseña;
        }
        public async Task InsertReseña(Resena direccion)
        {
            _reseña.Resenas.Add(direccion);
            await _reseña.SaveChangesAsync();
        }

        public async Task UpdateReseña(Resena direccion)
        {
            _reseña.Resenas.Update(direccion);
            await _reseña.SaveChangesAsync();
        }

        public async Task DeleteReseña(Resena direccion)
        {
            _reseña.Resenas.Remove(direccion);
            await _reseña.SaveChangesAsync();
        }
    }
}
