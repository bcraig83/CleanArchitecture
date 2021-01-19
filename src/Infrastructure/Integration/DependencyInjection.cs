using Application.Common.Interfaces;
using Infrastructure.Email;
using Infrastructure.Files;
using Integration;
using Integration.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration
                .GetSection(IntegrationOptions.AppSettingsFileLocation)
                .Get<IntegrationOptions>();
            services.AddScoped(x => options);

            // Just an example of how it might be used here...
            if (options.UseStaticDateTimeService)
            {
                services.AddTransient<IDateTimeService, StaticDateTimeService>();
            }
            else
            {
                services.AddTransient<IDateTimeService, DateTimeService>();
            }

            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}