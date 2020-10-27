using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public interface IApplicationDbContext
    {

        // Add your own DbSets for persisted entities here...
        //DbSet<TodoList> TodoLists { get; set; }

        //DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}