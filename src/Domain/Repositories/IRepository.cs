using Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        // Fill out this interface as we go along.
        // TODO: should these async methods accept a cancellation token?
        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> RemoveAsync(TEntity entity);
    }
}