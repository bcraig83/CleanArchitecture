using Application.Books.Commands.CreateBook;
using Application.Books.Commands.CreateBook.Services;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Books.Commands.CreateBook
{
    public class BookMapperTest
    {
        [Fact]
        public void ShouldMapAuthorToLowercase_WhenSetInConfig()
        {
            // Arrange
            var options = new ApplicationOptions
            {
                StoreAuthorInLowercase = true
            };
            var sut = new BookMapper(options);
            var command = new CreateBookCommand
            {
                Title = "Frank Sinatra: My Way",
                Author = "Karen Deeney",
                Language = "English",
                Publisher = "Penguin",
                ISBN10 = "1928374655"
            };

            // Act
            var result = sut.Map(command);

            // Assert
            result.ShouldNotBeNull();
            result.Author.ShouldBe("karen deeney");
        }
    }
}