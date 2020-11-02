using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly IApplicationDbContext _context;

        public TodoListRepository(
            IApplicationDbContext context)
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

        public async Task<IList<TodoList>> GetAllAsync()
        {
            return await _context.TodoLists.ToListAsync();
        }

        public async Task<TodoList> RemoveAsync(TodoList entity)
        {
            var result = _context.TodoLists.Remove(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<TodoList> UpdateAsync(TodoList entity)
        {
            var result = _context.TodoLists.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}