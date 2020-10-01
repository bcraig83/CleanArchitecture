using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITodoListRepository : IRepository<TodoList>
    {
        // Could probably be made generic. Use an 'object' instead of an int?
        Task<TodoList> FindByIdAsync(int id);
    }
}