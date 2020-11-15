using Application.Common.Exceptions;
using Application.TodoLists.Commands.CreateTodoList;
using Domain.Entities;
using Shouldly;
using System.Linq;
using Xunit;

namespace Application.IntegrationTests.Features.EntityFramework.TodoLists
{
    [Collection("Application test collection")]
    public class CreateTodoListFeature
    {
        private readonly ApplicationTestFixture _fixture;

        public CreateTodoListFeature(ApplicationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void ShouldThrowException_WhenMinimumFiledsAreNotFilledIn()
        {
            // Arrange
            var command = new CreateTodoListCommand();

            // Act
            var exception = (ValidationException)await Record.ExceptionAsync(async () =>
            {
                await _fixture.SendAsync(command);
            });

            // Assert
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldContain("One or more validation failures have occurred.");

            var errors = exception.Errors;
            errors.ShouldNotBeNull();

            errors.TryGetValue("Title", out string[] errorText);
            errorText.ShouldNotBeNull();
            errorText.Count().ShouldBe(1);
            errorText[0].ShouldBe("Title is required.");
        }

        [Fact]
        public async void ShouldThrowException_WhenCreatingListWithNonUniqueTitle()
        {
            // Arrange
            var createdListId = await _fixture.SendAsync(new CreateTodoListCommand
            {
                Title = "Shopping"
            });

            var createdEntity = await _fixture.FindAsync<TodoList>(createdListId);
            createdEntity.ShouldNotBeNull();

            // Act
            var exception = (ValidationException)await Record.ExceptionAsync(async () =>
            {
                var result = await _fixture.SendAsync(new CreateTodoListCommand
                {
                    Title = "Shopping"
                });
            });

            // Assert
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldContain("One or more validation failures have occurred.");

            var errors = exception.Errors;
            errors.ShouldNotBeNull();

            errors.TryGetValue("Title", out string[] errorText);
            errorText.ShouldNotBeNull();
            errorText.Count().ShouldBe(1);
            errorText[0].ShouldBe("The specified title already exists.");
        }

        [Fact]
        public async void ShouldCreateTodoList_WhenValidListIsReceived()
        {
            // Arrange
            var userId = await _fixture.RunAsDefaultUserAsync();
            var command = new CreateTodoListCommand()
            {
                Title = "New List"
            };

            // Act
            var createdTodoListId = await _fixture.SendAsync(command);

            // Assert
            var createdList = await _fixture.FindAsync<TodoList>(createdTodoListId);
            createdList.ShouldNotBeNull();
            createdList.Title.ShouldBe(command.Title);
            createdList.CreatedBy.ShouldBe(userId);
        }
    }
}