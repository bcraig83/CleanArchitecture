using DataAccess.EntityFramework;
using DataAccess.EntityFramework.Repositories;
using DataAccess.InMemory;
using Domain.Repositories;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration
                .GetSection(DataAccessOptions.AppSettingsFileLocation)
                .Get<DataAccessOptions>();
            services.AddScoped(x => options);

            services
                .AddAuthentication()
                .AddIdentityServerJwt();

            switch (options.PersistenceMechanism)
            {
                case "EntityFramework":
                    services.AddPersistenceThroughEntityFramework(configuration, options);
                    break;

                default:
                    services.AddPersistenceThroughInMemoryDatastore();
                    break;
            }

            return services;
        }

        // Obviously this is just for testing
        private static IServiceCollection AddPersistenceThroughInMemoryDatastore(
            this IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));

            services.AddScoped<EventProcessor>();

            return services;
        }

        private static IServiceCollection AddPersistenceThroughEntityFramework(
           this IServiceCollection services,
           IConfiguration configuration,
           DataAccessOptions options)
        {
            if (options.UseEntityFrameworkInMemoryDatabase)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("caSampleDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services
                .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services
                .AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient(typeof(IRepository<>), typeof(EnitityFrameworkRepository<>));

            return services;
        }
    }
}