using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.FunctionalTests.Fakes
{
    public class TodoItemRepositoryFake : ITodoItemRepository
    {
        // Implement as testing requires...
        private IDictionary<int, TodoItem> _dataStore;

        public TodoItemRepositoryFake()
        {
            _dataStore = new Dictionary<int, TodoItem>();
        }

        public Task<TodoItem> AddAsync(TodoItem entity)
        {
            _dataStore.Add(entity.Id, entity);
            return Task.FromResult(entity);
        }

        public Task<TodoItem> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<TodoItem> GetAll()
        {
            return _dataStore.Values.AsQueryable();
        }

        public Task<TodoItem> RemoveAsync(TodoItem entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            throw new System.NotImplementedException();
        }
    }
}