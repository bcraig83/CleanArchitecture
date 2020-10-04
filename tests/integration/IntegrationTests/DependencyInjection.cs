using Application;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using IntegrationTests.Fakes;
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

            AddApplication();
            AddDatabase();

            // TODO: add additional dependencies as required....
            _services.AddScoped<ICurrentUserService, CurrentUserServiceFake>();
            _services.AddScoped<IDateTime, DateTimeFake>();

            ServiceProvider = _services.BuildServiceProvider();
        }

        private void AddApplication()
        {
            _services.AddApplication();
        }

        private void AddDatabase()
        {
            _services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));

            _services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetService<ApplicationDbContext>());

            //_services
            //    .AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //_services
            //    .AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
        }
    }
}