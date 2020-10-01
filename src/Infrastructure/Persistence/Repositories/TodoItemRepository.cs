using Domain.Entities;
using Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    // TODO: all this code needs to be improved to provide better negative path funtionality, exception handling, etc.
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IApplicationDbContext _context;

        public TodoItemRepository(
            IApplicationDbContext context)
        {
            _context = context;
        }

        // TODO: feels like alot of the basic CRUD stuff could go into a base repository class?
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

        public async Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            var result = _context.TodoItems.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}