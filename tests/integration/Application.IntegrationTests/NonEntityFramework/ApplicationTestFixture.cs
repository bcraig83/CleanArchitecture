using Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.IO;
using System.Linq;
using WebApi;

namespace Application.IntegrationTests.NonEntityFramework
{
    public class ApplicationTestFixture : IDisposable
    {
        private IConfigurationRoot Configuration { get; set; }
        public IServiceScopeFactory ScopeFactory { get; private set; }

        public string CurrentUserId { get; private set; }

        public ApplicationTestFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var startup = new Startup(Configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "WebApi"));

            services.AddLogging();

            startup.ConfigureServices(services);

            // Replace service registration for ICurrentUserService
            // Remove existing registration
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            // Register testing version
            services.AddTransient(provider =>
                Mock.Of<ICurrentUserService>(s => s.UserId == CurrentUserId));

            ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}