using Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public abstract class BaseInMemoryDatabaseTests
    {
        protected IMediator Mediator;

        // TODO: not sure if this needs to be a member variable? We'll see...
        protected ServiceProvider ServiceProvider;

        protected BaseInMemoryDatabaseTests()
        {
            ServiceProvider = DependencyInjection.Instance.ServiceProvider;

            Mediator = (IMediator)ServiceProvider.GetService(typeof(IMediator));

            Seed();
        }

        private void Seed()
        {
            var context = (ApplicationDbContext)ServiceProvider.GetService(typeof(ApplicationDbContext));

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // TODO: First, this is where you might seed standard reference data in the database.
            // <standard_code_here>

            SeedFeatureSpecificData(context);
        }

        // If a feature requires it's own specific seed data in the database,
        // override this virtual method and perform the seeding there.
        protected virtual void SeedFeatureSpecificData(ApplicationDbContext context)
        {
        }
    }
}