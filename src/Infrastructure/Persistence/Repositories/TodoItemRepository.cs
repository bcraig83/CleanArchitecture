using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IApplicationDbContext _context;

        public TodoItemRepository(
            IApplicationDbContext context)
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

        public async Task<IList<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
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