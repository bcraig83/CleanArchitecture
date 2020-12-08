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
            services
                .AddAuthentication()
                .AddIdentityServerJwt();

            if (configuration.GetValue<bool>("UseInMemoryPersistence"))
            {
                services.AddPersistenceThroughInMemoryDatastore();
            }
            else
            {
                services.AddPersistenceThroughEntityFramework(configuration);
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
           IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
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