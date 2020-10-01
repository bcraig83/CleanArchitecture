using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.FunctionalTests
{
    public abstract class BaseFeature
    {
        public BaseFeature(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<ApplicationDbContext> ContextOptions { get; }

        protected virtual void Seed()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            { 
            }
        }
    }
}