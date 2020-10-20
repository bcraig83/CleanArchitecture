using Application.TodoItems.Commands.CreateTodoItem;
using Application.TodoItems.Commands.UpdateTodoItem;
using Application.TodoLists.Queries.GetTodos.Models;
using Newtonsoft.Json;
using Shouldly;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xbehave;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    public class TodoListsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string RequestUri = "/api/TodoLists";

        private readonly HttpClient _client;

        public TodoListsControllerIntegrationTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Scenario]
        public void ShouldHandleCrudOperations()
        {
            "Given that there are initially no lists setup in the system"
                .x(async () =>
                {
                    var httpResponse = await _client.GetAsync(RequestUri);

                    httpResponse.EnsureSuccessStatusCode();

                    var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                    var todosVm = JsonConvert.DeserializeObject<TodosVm>(stringResponse);

                    var lists = todosVm.Lists;
                    lists.Count.ShouldBe(0);
                });

            "When we create a valid list"
                .x(async () =>
                {
                    var command = new CreateTodoItemCommand
                    {
                        Title = "Hello world list"
                    };

                    var stringContent = new StringContent(
                        JsonConvert.SerializeObject(command),
                        Encoding.UTF8,
                        "application/json");

                    var httpResponse = await _client.PostAsync(
                        RequestUri, stringContent);

                    httpResponse.EnsureSuccessStatusCode();
                });

            "Then the list should be retrievable from the system"
                   .x(async () =>
                   {
                       var httpResponse = await _client.GetAsync(RequestUri);

                       httpResponse.EnsureSuccessStatusCode();

                       var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                       var todosVm = JsonConvert.DeserializeObject<TodosVm>(stringResponse);

                       var lists = todosVm.Lists;
                       lists.Count.ShouldBe(1);

                       var list = lists.First();
                       list.Title.ShouldBe("Hello world list");
                   });

            "When we update that list with a different title"
                .x(async () =>
                {
                    var command = new UpdateTodoItemCommand
                    {
                        Id = 1,
                        Title = "Hello List Number 2"
                    };

                    var stringContent = new StringContent(
                        JsonConvert.SerializeObject(command),
                        Encoding.UTF8,
                        "application/json");

                    var httpResponse = await _client.PutAsync(
                        RequestUri, stringContent);

                    httpResponse.EnsureSuccessStatusCode();
                });

            "Then the list should have been updated in the system"
                .x(async () =>
                {
                    var httpResponse = await _client.GetAsync(RequestUri);

                    httpResponse.EnsureSuccessStatusCode();

                    var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                    var todosVm = JsonConvert.DeserializeObject<TodosVm>(stringResponse);

                    var lists = todosVm.Lists;
                    lists.Count.ShouldBe(1);

                    var list = lists.First();
                    list.Title.ShouldBe("Hello List Number 2");
                });

            "When we delete that list"
                .x(async () =>
                {
                    var httpResponse = await _client.DeleteAsync(RequestUri + "/1");

                    httpResponse.EnsureSuccessStatusCode();
                });

            "Then the list should have been removed from the system"
                .x(async () =>
                {
                    var httpResponse = await _client.GetAsync(RequestUri);

                    httpResponse.EnsureSuccessStatusCode();

                    var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                    var todosVm = JsonConvert.DeserializeObject<TodosVm>(stringResponse);

                    var lists = todosVm.Lists;
                    lists.Count.ShouldBe(0);
                });
        }
    }
}