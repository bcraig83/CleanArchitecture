using Application.TodoItems.Commands.CreateTodoItem;
using Shouldly;
using System.Linq;
using Xunit;

namespace Application.UnitTests.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidatorTests
    {
        [Fact]
        public void ShouldHaveErrorWhenTitleIsGreaterThan200Characters()
        {
            // Arrange
            var itemUnderTest = new CreateTodoItemCommandValidator();
            var command = new CreateTodoItemCommand
            {
                Title = "ABCDEFGABCDEFGABCDEFGABCDEFGABCDEFGABCDEFGABCDEFG" +
                "ABCDEFGABCDEFGABCDEFGABCDEFGABCDEFGABCDEFGABCDEFGABCDEFG" +
                "ABCDEFGABCDEFGABCDEFGABCDEFGABCDEFGABCDEFGABCDEFGABCDEFG" +
                "ABCDEFGABCDEFGABCDEFGABCDEFGABCDEFGABCDEFG"
            };

            // Act
            var result = itemUnderTest.Validate(command);

            // Assert
            var boolResult = result.IsValid;
            boolResult.ShouldBeFalse();

            var errors = result.Errors;
            errors.ShouldNotBeNull();
            errors.Count.ShouldBe(1);

            var error = errors.First();
            error.ErrorMessage.ShouldBe("The length of 'Title' must be 200 characters or fewer. You entered 203 characters.");
            error.PropertyName.ShouldBe("Title");
        }
    }
}