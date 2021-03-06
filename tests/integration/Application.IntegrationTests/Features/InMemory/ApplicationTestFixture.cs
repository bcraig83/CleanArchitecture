﻿using Application.Common.Interfaces;
using Application.IntegrationTests.Fakes;
using Domain.Common;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace Application.IntegrationTests.Features.InMemory
{
    public class ApplicationTestFixture : IDisposable
    {
        private IConfigurationRoot Configuration { get; set; }
        public IServiceScopeFactory ScopeFactory { get; private set; }

        public string CurrentUserId { get; private set; }

        private readonly EmailSenderStub _emailSenderStub;

        public ApplicationTestFixture()
        {
            // TODO: that app settings file should be called "appsettings.IntegrationTest.json"
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var startup = new Startup(Configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "WebApi"));

            services.AddLogging();

            startup.ConfigureServices(services);

            // Replace service registration for email sender
            var currentEmailSenderServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IEmailSender));
            services.Remove(currentEmailSenderServiceDescriptor);

            // Register testing version
            _emailSenderStub = new EmailSenderStub();
            services.AddTransient<IEmailSender>(provider => _emailSenderStub);

            ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = ScopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : BaseEntity
        {
            using var scope = ScopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetService<IRepository<TEntity>>();

            var result = await repository.GetAsync(id);

            return result;
        }

        public async Task<int> AddAsync<TEntity>(TEntity entity)
           where TEntity : BaseEntity
        {
            using var scope = ScopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetService<IRepository<TEntity>>();
            var result = await repository.AddAsync(entity);
            return result.Id;
        }

        public async Task RemoveAsync<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            using var scope = ScopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetService<IRepository<TEntity>>();
            await repository.RemoveAsync(entity);
        }

        public void Dispose()
        {
        }

        public void ClearRecordedEmails()
        {
            _emailSenderStub.Clear();
        }

        public IList<EmailDetails> GetRecordedEmails()
        {
            return _emailSenderStub._recordedEmails;
        }
    }

    [CollectionDefinition("Non EF application test collection")]
    public class ApplicationTestCollection : ICollectionFixture<ApplicationTestFixture>
    {
    }
}