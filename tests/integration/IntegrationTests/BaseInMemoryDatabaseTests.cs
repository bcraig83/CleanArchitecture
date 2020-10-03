using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public abstract class BaseInMemoryDatabaseTests
    {
        //protected DbContextOptions<ApplicationDbContext> ContextOptions { get; }
        //protected ServiceProvider ServiceProvider;

        //protected BaseInMemoryDatabaseTests(
        //    DbContextOptions<ApplicationDbContext> contextOptions)
        //{
        //    ServiceProvider = DependencyInjection.Instance.BuildServiceProvider();

        //    ContextOptions = contextOptions;

        //    Seed();
        //}

        //private void Seed()
        //{
        //    // TODO: this is where you might seed standard reference data in the database.
        //    using (var context = new ApplicationDbContext(ContextOptions))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        SeedFeatureSpecificData(context);
        //    }
        //}

        //// If a feature requires it's own specific seed data in the database,
        //// override this virtual method and perform the seeding there.
        //protected virtual void SeedFeatureSpecificData(ApplicationDbContext context)
        //{
        //}
    }
}