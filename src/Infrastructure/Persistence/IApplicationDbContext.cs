using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    // TODO: I really don't like how EFCore is embedded into the application layer! Would like to abstract this away eventually...
    public interface IApplicationDbContext
    {

        // Add your own DbSets for persisted entities here...
        //DbSet<TodoList> TodoLists { get; set; }

        //DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}