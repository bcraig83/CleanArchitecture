using Domain.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        // Fill out this interface as we go along.

        IQueryable<TEntity> GetAll();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> RemoveAsync(TEntity entity);
    }
}