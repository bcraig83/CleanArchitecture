using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.FunctionalTests.Fakes
{
    public class TodoItemRepositoryFake : ITodoItemRepository
    {
        private readonly IDictionary<int, TodoItem> _dataStore;
        private static int id = 0;

        public TodoItemRepositoryFake()
        {
            _dataStore = new Dictionary<int, TodoItem>();
        }

        // Implement as testing requires...
        public Task<TodoItem> AddAsync(TodoItem entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = id++;
            }

            _dataStore.Add(entity.Id, entity);
            return Task.FromResult(entity);
        }

        public Task<TodoItem> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<TodoItem> RemoveAsync(TodoItem entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<TodoItem>> GetAllAsync()
        {
            return Task.FromResult((IList<TodoItem>)
                _dataStore.Values.ToList());
        }
    }
}