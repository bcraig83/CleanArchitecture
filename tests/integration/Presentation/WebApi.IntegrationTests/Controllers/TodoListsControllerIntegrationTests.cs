using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    public class TodoListsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TodoListsControllerIntegrationTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldGetTodos_WhenGetIsCalled()
        {
            // TODO: add any required API level integration tests, e.g.:

            //// The endpoint or route of the controller action.
            //var httpResponse = await _client.GetAsync("/api/TodoLists");

            //// Must be successful.
            //httpResponse.EnsureSuccessStatusCode();

            //// Deserialize and examine results.
            //var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            //var todosVm = JsonConvert.DeserializeObject<TodosVm>(stringResponse);

            //todosVm.ShouldNotBeNull();
            //todosVm.PriorityLevels.ShouldNotBeNull();

            //var lists = todosVm.Lists;
            //lists.ShouldNotBeNull();
            //lists.Count.ShouldBe(1);

            //var list = lists.First();
            //list.ShouldNotBeNull();
            //list.Title.ShouldBe("Monday List");

            //var items = list.Items;
            //items.ShouldNotBeNull();
            //items.Count.ShouldBe(1);

            //var item = items.First();
            //item.Done.ShouldBeFalse();
            //item.Id.ShouldBe(1);
            //item.ListId.ShouldBe(1);
            //item.Priority.ShouldBe(2);
            //item.Title.ShouldBe("Buy bread");
        }
    }
}