using Microsoft.EntityFrameworkCore;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly VentasLimpiezaContext _context;
        protected readonly DbSet<T> _entities;
        public BaseRepository(VentasLimpiezaContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
           await _entities.AddAsync(entity);
            //await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
           // await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
            //await _context.SaveChangesAsync();
        }
    }
}
