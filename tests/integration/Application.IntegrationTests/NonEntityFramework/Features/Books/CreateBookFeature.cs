using Xunit;

namespace Application.IntegrationTests.NonEntityFramework.Features.Books
{
    [Collection("Non EF application test collection")]
    public class CreateBookFeature
    {
        private readonly ApplicationTestFixture _fixture;

        public CreateBookFeature(ApplicationTestFixture fixture)
        {
            _fixture = fixture;
        }

        //[Theory]
        //[InlineData("")]
        //[InlineData(null)]
        //public async void ShouldThrowException_WhenTitleIsInvalid(string title)
        //{
        //    // Arrange
        //    var command = new CreateBookCommand
        //    {
        //        Title = title
        //    };

        //    // Act
        //    var exception = (ValidationException)await Record.ExceptionAsync(async () =>
        //    {
        //        var result = await _fixture.SendAsync(command);
        //    });

        //    // Assert
        //    exception.ShouldBeOfType<ValidationException>();
        //    exception.Message.ShouldContain("One or more validation failures have occurred.");

        //    var errors = exception.Errors;
        //    errors.ShouldNotBeNull();

        //    errors.TryGetValue("Title", out string[] errorText);
        //    errorText.ShouldNotBeNull();
        //    errorText.Count().ShouldBe(1);
        //    errorText[0].ShouldBe("Cannot create a book without a title");
        //}

        //[Theory]
        //[InlineData("")]
        ////TODO: [InlineData(null)]
        //[InlineData("1")]
        //[InlineData("123456789101112")]
        //[InlineData("ABCD")]
        //[InlineData("ABCDEFGHIJ")]
        //public async void ShouldThrowException_WhenIsbnIsInvalid(string isbn)
        //{
        //    // Arrange
        //    var command = new CreateBookCommand
        //    {
        //        Title = "Some Valid Title",
        //        ISBN10 = isbn
        //    };

        //    // Act
        //    var exception = (ValidationException)await Record.ExceptionAsync(async () =>
        //    {
        //        var result = await _fixture.SendAsync(command);
        //    });

        //    // Assert
        //    exception.ShouldBeOfType<ValidationException>();
        //    exception.Message.ShouldContain("One or more validation failures have occurred.");

        //    var errors = exception.Errors;
        //    errors.ShouldNotBeNull();

        //    errors.TryGetValue("ISBN10", out string[] errorText);
        //    errorText.ShouldNotBeNull();
        //    errorText.Count().ShouldBe(1);
        //    errorText[0].ShouldBe("ISBN10 code must be made up of 10 digits");
        //}
    }
}