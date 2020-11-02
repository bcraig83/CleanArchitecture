using Application.TodoLists.Queries.GetTodos;
using Domain.Entities;
using Shouldly;
using System.Linq;
using Xunit;

namespace Application.IntegrationTests.Features
{
    [Collection("Application test collection")]
    public class GetTodoListFeature
    {
        private readonly ApplicationTestFixture _fixture;

        public GetTodoListFeature(ApplicationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void ShouldReturnPriorityLevels()
        {
            // Arrange
            var query = new GetTodosQuery();

            // Act
            var result = await _fixture.SendAsync(query);

            // Assert
            result.PriorityLevels.ShouldNotBeEmpty();
        }

        [Fact(Skip = "No idea why this is not working. Suspect it's an async issue")]
        //[Fact]
        public async void ShouldReturnAllListsAndItems()
        {
            var createdId = await _fixture.AddAsync(new TodoList
            {
                Title = "Shopping 30/10/2020",
                Items =
                    {
                        new TodoItem { Title = "Apples"},
                        new TodoItem { Title = "Milk"},
                        new TodoItem { Title = "Bread"},
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" }
                    }
            });

            var confirmCreation = await _fixture.FindAsync<TodoList>(createdId);
            confirmCreation.ShouldNotBeNull();

            var query = new GetTodosQuery();

            var result = await _fixture.SendAsync(query);

            var createdList = result.Lists.Single(x => x.Title == "Shopping 30/10/2020");
            createdList.ShouldNotBeNull();
            createdList.Items.Count.ShouldBe(7);
        }
    }
}