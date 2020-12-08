using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public interface IApplicationDbContext
    {

        //DbSet<TodoList> TodoLists { get; set; }
        //DbSet<TodoItem> TodoItems { get; set; }
        //DbSet<Book> Books { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}