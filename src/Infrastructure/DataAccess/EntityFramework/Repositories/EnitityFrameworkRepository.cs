using Domain.Common;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Repositories
{
    public class EnitityFrameworkRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public EnitityFrameworkRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = _context.Add(entity).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var result = await _context.FindAsync<TEntity>(id);
            return result;
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            var set = _context.Set<TEntity>();
            var result = await set.ToListAsync();
            return result;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            var result = _context.Remove(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = _context.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}