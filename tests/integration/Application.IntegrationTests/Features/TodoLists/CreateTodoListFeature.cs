using Xunit;

namespace Application.IntegrationTests.Features.TodoLists
{
    [Collection("Application test collection")]
    public class CreateTodoListFeature
    {
        private readonly ApplicationTestFixture _fixture;

        public CreateTodoListFeature(ApplicationTestFixture fixture)
        {
            _fixture = fixture;
        }

        //[Fact]
        //public async void ShouldThrowException_WhenMinimumFiledsAreNotFilledIn()
        //{
        //    // Arrange
        //    var command = new CreateTodoListCommand();

        //    // Act
        //    var exception = (ValidationException)await Record.ExceptionAsync(async () =>
        //    {
        //        await _fixture.SendAsync(command);
        //    });

        //    // Assert
        //    exception.ShouldBeOfType<ValidationException>();
        //    exception.Message.ShouldContain("One or more validation failures have occurred.");

        //    var errors = exception.Errors;
        //    errors.ShouldNotBeNull();

        //    errors.TryGetValue("Title", out string[] errorText);
        //    errorText.ShouldNotBeNull();
        //    errorText.Count().ShouldBe(1);
        //    errorText[0].ShouldBe("Title is required.");
        //}
    }
}