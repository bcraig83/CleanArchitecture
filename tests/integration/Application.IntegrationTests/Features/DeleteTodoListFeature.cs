using Application.Common.Exceptions;
using Application.TodoLists.Commands.CreateTodoList;
using Application.TodoLists.Commands.DeleteTodoList;
using Domain.Entities;
using Shouldly;
using Xunit;

namespace Application.IntegrationTests.Features
{
    [Collection("Application test collection")]
    public class DeleteTodoListFeature
    {
        private readonly ApplicationTestFixture _fixture;

        public DeleteTodoListFeature(ApplicationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void ShouldThrowException_WhenTryingToDeleteListThatDoesNotExist()
        {
            // Arrange
            var command = new DeleteTodoListCommand { Id = 99 };

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
        public async void ShouldDeleteList_WhenItExistsInTheDatabase()
        {
            // Arrange
            var listId = await _fixture.SendAsync(new CreateTodoListCommand
            {
                Title = "List to Delete"
            });

            // Act
            await _fixture.SendAsync(new DeleteTodoListCommand
            {
                Id = listId
            });

            // Assert
            var list = await _fixture.FindAsync<TodoList>(listId);
            list.ShouldBeNull();
        }
    }
}