using Application.Common.Interfaces;
using Infrastructure.Email;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegration(
            this IServiceCollection services)
        {
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}