using Application.Common.Interfaces;
using Infrastructure.Email;
using Infrastructure.Identity;
using Infrastructure.Persistence.EntityFramework;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IEmailSender, EmailSender>();

            services
                .AddAuthentication()
                .AddIdentityServerJwt();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            // TODO: this will be driven by some config item
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

        // TODO: obviously this is just for testing
        private static IServiceCollection AddPersistenceThroughInMemoryDatastore(
            this IServiceCollection services)
        {
            //services.AddSingleton<IRepository<TodoItem>, InMemoryRepository<TodoItem>>();
            //services.AddSingleton<IRepository<TodoList>, InMemoryRepository<TodoList>>();
            //services.AddSingleton<IRepository<Book>, InMemoryRepository<Book>>();

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

            // Add your own repositories, e.g:
            //services.AddTransient<IRepository<TodoItem>, EnitityFrameworkRepository<TodoItem>>();

            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}