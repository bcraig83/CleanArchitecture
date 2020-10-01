using Domain.Entities;
using Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly IApplicationDbContext2 _context;

        public TodoListRepository(
            IApplicationDbContext2 context)
        {
            _context = context;
        }

        public async Task<TodoList> AddAsync(TodoList entity)
        {
            var result = _context.TodoLists
                .Add(entity)
                .Entity;

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<TodoList> FindByIdAsync(int id)
        {
            var result = await _context.TodoLists.FindAsync(id);
            return result;
        }

        public IQueryable<TodoList> GetAll()
        {
            return _context.TodoLists;
        }

        public async Task<TodoList> RemoveAsync(TodoList entity)
        {
            var result = _context.TodoLists.Remove(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public Task<TodoList> UpdateAsync(TodoList entity)
        {
            // TODO: currently not needed anywhere, but may be needed in the future.
            throw new System.NotImplementedException();
        }
    }
}