using Domain.Entities;
using Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    // TODO: all this code needs to be improved to provide better negative path funtionality, exception handling, etc.
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IApplicationDbContext2 _context;

        public TodoItemRepository(
            IApplicationDbContext2 context)
        {
            _context = context;
        }

        public async Task<TodoItem> AddAsync(TodoItem entity)
        {
            var result = _context.TodoItems
                   .Add(entity)
                   .Entity;

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<TodoItem> FindByIdAsync(int id)
        {
            var result = await _context.TodoItems.FindAsync(id);
            return result;
        }

        public IQueryable<TodoItem> GetAll()
        {
            return _context.TodoItems;
        }

        public async Task<TodoItem> RemoveAsync(TodoItem entity)
        {
            var result = _context.TodoItems.Remove(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            // TODO: currently not needed anywhere, but may be needed in the future.
            throw new System.NotImplementedException();
        }
    }
}