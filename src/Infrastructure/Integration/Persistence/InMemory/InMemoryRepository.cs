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
        private readonly EventProcessor _eventProcessor;

        private static int index = 1;

        public InMemoryRepository(
            EventProcessor eventProcessor)
        {
            _dataStore = new Dictionary<int, TEntity>();
            _eventProcessor = eventProcessor;
        }

        // TODO: this is obviously very rough and ready, and needs proper defensive code!
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = index++;
            }

            _dataStore.TryAdd(entity.Id, entity);

            await _eventProcessor.ProcessEvents(entity);

            return entity;
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

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            _dataStore.Remove(entity.Id);
            await _eventProcessor.ProcessEvents(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dataStore.Remove(entity.Id);
            _dataStore.TryAdd(entity.Id, entity);

            await _eventProcessor.ProcessEvents(entity);

            return entity;
        }
    }
}