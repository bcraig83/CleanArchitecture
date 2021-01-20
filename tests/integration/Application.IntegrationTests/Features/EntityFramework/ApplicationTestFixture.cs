using DataAccess.EntityFramework;
using Domain.Common;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace Application.IntegrationTests.Features.EntityFramework
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
                .AddJsonFile("appsettings.EntityFramework.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var startup = new Startup(Configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "EntityFramework" &&
                w.ApplicationName == "WebApi"));

            services.AddLogging();

            startup.ConfigureServices(services);

            ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            EnsureDatabase();
        }

        private void EnsureDatabase()
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.EnsureCreated();
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = ScopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public async Task<string> RunAsDefaultUserAsync()
        {
            return await RunAsUserAsync("test@local", "Testing1234!");
        }

        public async Task<string> RunAsUserAsync(string userName, string password)
        {
            using var scope = ScopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            var existingUser = await userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                return existingUser.Id;
            }

            var user = new ApplicationUser { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                CurrentUserId = user.Id;

                return CurrentUserId;
            }

            var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

            throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
        }

        public async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : BaseEntity
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public async Task<int> AddAsync<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task RemoveAsync<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Remove(entity);

            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.EnsureDeleted();
        }
    }

    [CollectionDefinition("Application test collection")]
    public class ApplicationTestCollection : ICollectionFixture<ApplicationTestFixture>
    {
    }
}