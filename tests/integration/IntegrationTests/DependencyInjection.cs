using Application;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class DependencyInjection
    {
        private static DependencyInjection _instance = null;

        private readonly IServiceCollection _services = null;

        public ServiceProvider ServiceProvider { get; private set; } = null;

        public static DependencyInjection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DependencyInjection();
                }
                return _instance;
            }
        }

        private DependencyInjection()
        {
            _services = new ServiceCollection();

            _services.AddApplication();

            _services.AddScoped(opt =>
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options);

            // TODO: add additional dependencies as required....

            ServiceProvider = _services.BuildServiceProvider();
        }
    }
}