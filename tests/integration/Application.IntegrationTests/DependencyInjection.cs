using Application;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using IntegrationTests.Fakes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace IntegrationTests
{
    public class DependencyInjection
    {
        private static DependencyInjection _instance = null;
        private readonly IServiceCollection _services = null;
        public ServiceProvider ServiceProvider { get; private set; } = null;

        public Mock<IIdentityService> IdentityServiceMock = new Mock<IIdentityService>();

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

            _services.AddLogging();

            AddApplication();
            AddDatabase();

            // Add additional dependencies as required....
            // Could just use mocks for these, I'm currently undecided as to which approach is best...
            _services.AddScoped<ICurrentUserService, CurrentUserServiceStub>();
            _services.AddScoped<IDateTime, DateTimeStub>();
            _services.AddScoped<IEmailSender, EmailSenderSpy>();
            _services.AddScoped(x => IdentityServiceMock.Object);

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

            // Add your repositories here...
            //_services.AddTransient<ITodoItemRepository, TodoItemRepository>();
        }
    }
}