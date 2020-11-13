using Application.Common.Exceptions;
using Application.IntegrationTests.EntityFramework;
using Application.TodoLists.Commands.CreateTodoList;
using Application.TodoLists.Commands.UpdateTodoList;
using Domain.Entities;
using Shouldly;
using System.Linq;
using Xunit;

namespace Application.IntegrationTests.EntityFramework.Features.TodoLists
{
    [Collection("Application test collection")]
    public class UpdateTodoListFeature
    {
        private readonly ApplicationTestFixture _fixture;

        public UpdateTodoListFeature(ApplicationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void ShouldThrowNotFoundException_WhenIdDoesNotExistInDatabase()
        {
            // Arrange
            var command = new UpdateTodoListCommand
            {
                Id = 99,
                Title = "New Title 31/10/2020"
            };

            // Act
            var exception = await Record.ExceptionAsync(async () =>
            {
                await _fixture.SendAsync(command);
            });

            // Assert
            exception.ShouldBeOfType<NotFoundException>();
            exception.Message.ShouldContain("Entity \"TodoList\" (99) was not found.");
        }

        [Fact]
        public async void ShouldThrowException_WhenTitleAlreadyExists()
        {
            // Arrange
            var listId = await _fixture.SendAsync(new CreateTodoListCommand
            {
                Title = "New List 17:10"
            });

            await _fixture.SendAsync(new CreateTodoListCommand
            {
                Title = "Other List 17:12"
            });

            var command = new UpdateTodoListCommand
            {
                Id = listId,
                Title = "Other List 17:12"
            };

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
            errorText[0].ShouldBe("The specified title already exists.");
        }

        [Fact]
        public async void ShouldUpdateTodoList_WhenValidListIsSentIn()
        {
            var userId = await _fixture.RunAsDefaultUserAsync();

            var listId = await _fixture.SendAsync(new CreateTodoListCommand
            {
                Title = "New List 17:25"
            });

            var command = new UpdateTodoListCommand
            {
                Id = listId,
                Title = "Updated List Title 17:25"
            };

            await _fixture.SendAsync(command);

            var list = await _fixture.FindAsync<TodoList>(listId);

            list.Title.ShouldBe(command.Title);
            list.LastModifiedBy.ShouldNotBeNull();
            list.LastModifiedBy.ShouldBe(userId);
            list.LastModified.ShouldNotBeNull();
        }
    }
}