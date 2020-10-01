using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        // TODO: do we need to include "ById" in this method name?
        Task<TodoItem> FindByIdAsync(int id);
    }
}
