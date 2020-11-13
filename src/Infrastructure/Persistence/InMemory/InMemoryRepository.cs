using Domain.Common;
using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.InMemory
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IDictionary<int, TEntity> _dataStore;

        public InMemoryRepository()
        {
            _dataStore = new Dictionary<int, TEntity>();
        }

        // TODO: this is obviousyl very rough and ready, and needs proper defensive code!
        public Task<TEntity> AddAsync(TEntity entity)
        {
            _dataStore.TryAdd(entity.Id, entity);
            return Task.FromResult(entity);
        }

        public Task<IList<TEntity>> GetAllAsync()
        {
            var resultAsCollection = _dataStore.Values;
            IList<TEntity> resultAsList = resultAsCollection.ToList();
            return Task.FromResult(resultAsList);
        }

        public Task<TEntity> GetAsync(int id)
        {
            _dataStore.TryGetValue(id, out var result);
            return Task.FromResult(result);
        }

        public Task<TEntity> RemoveAsync(TEntity entity)
        {
            _dataStore.Remove(entity.Id);
            return Task.FromResult(entity);
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dataStore.Remove(entity.Id);
            _dataStore.TryAdd(entity.Id, entity);
            return Task.FromResult(entity);
        }
    }
}