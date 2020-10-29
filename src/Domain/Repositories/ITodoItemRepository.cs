using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<TodoItem> FindByIdAsync(int id);
    }
}