using Application.Books.Queries.GetBooks;
using Domain.Entities;
using Shouldly;
using System.Linq;
using Xunit;

namespace Application.IntegrationTests.Features.Books
{
    [Collection("Application test collection")]
    public class GetBooksFeature
    {
        private readonly ApplicationTestFixture _fixture;

        public GetBooksFeature(ApplicationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void ShouldReturnAllBooksFromTheRepository()
        {
            // Arrange
            var bookToCreate = new Book
            {
                Title = "The wind in the willows",
                ISBN10 = "1515151515"
            };

            var createdId = await _fixture.AddAsync(bookToCreate);

            var query = new GetBooksQuery();

            // Act
            var result = await _fixture.SendAsync(query);

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);

            var createdBook = result.Single(x => x.Id == createdId);
            createdBook.ShouldNotBeNull();
            createdBook.ISBN10.ShouldBe("1515151515");
            createdBook.Title.ShouldBe("The wind in the willows");
        }
    }
}