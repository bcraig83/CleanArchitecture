using Application.FunctionalTests.Fakes;
using Application.TodoItems.Commands.CreateTodoItem;
using Domain.Repositories;
using MediatR;
using Shouldly;
using System.Linq;
using System.Threading;
using Xbehave;

namespace Application.FunctionalTests.Features
{
    public class TodoFeature
    {
        [Scenario]
        public void ShouldBeAbleToCreateTodoItem()
        {
            CreateTodoItemCommand command = null;
            ITodoItemRepository repository = null;
            IRequestHandler<CreateTodoItemCommand, int> systemUnderTest = null;
            int result;

            "Given the application is setup correctly"
                .x(() =>
                {
                    repository = new TodoItemRepositoryFake();
                    systemUnderTest = new CreateTodoItemCommandHandler(repository);
                });

            "And given no Todos are currently stored in the databas"
                .x(() =>
                {
                    repository.GetAll().Count().ShouldBe(0);
                });

            "And given a valid Todo item, with the title 'Buy milk'"
                .x(() =>
                {
                    command = new CreateTodoItemCommand
                    {
                        ListId = 1,
                        Title = "Buy milk"
                    };
                });

            "When that item is added via the application"
                .x(async () =>
                {
                    result = await systemUnderTest.Handle(command, CancellationToken.None);
                });

            "Then an item should now be in the database"
                .x(() =>
                {
                    repository
                        .GetAll()
                        .Count()
                        .ShouldBe(1);
                });

            "And that item should contain the correct title"
                .x(() =>
                {
                    repository
                        .GetAll()
                        .FirstOrDefault()
                        .Title
                        .ShouldBe("Buy milk");
                });
        }
    }
}