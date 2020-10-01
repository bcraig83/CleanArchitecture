using Application.Common.Interfaces;
using Infrastructure.Email;
using Infrastructure.Files;
using Infrastructure.Identity;
using Infrastructure.Persistence;
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
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext2>(options =>
                    options.UseInMemoryDatabase("caSampleDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext2>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext2).Assembly.FullName)));
            }

            services
                .AddScoped<IApplicationDbContext2>(provider => provider.GetService<ApplicationDbContext2>());

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext2>();

            services
                .AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext2>();

            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IIdentityService, IdentityService>();

            services
                .AddAuthentication()
                .AddIdentityServerJwt();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}