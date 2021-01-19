using Application.Books.Commands.CreateBook;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Moq.AutoMock;
using Shouldly;
using System.Threading;
using Xunit;

namespace Application.UnitTests.Books.Commands.CreateBook
{
    public class CreateBookCommandHandlerTest
    {
        [Fact]
        public async void ShouldStoreAuthorInLower_WhenSetInConfig()
        {
            // Arrange
            var automocker = new AutoMocker();

            var options = new ApplicationOptions
            {
                StoreAuthorInLowercase = true
            };
            automocker.Use(options);

            var sut = automocker.CreateInstance<CreateBookCommandHandler>();
            var command = new CreateBookCommand
            {
                Title = "Frank Sinatra: My Way",
                Author = "Karen Deeney",
                Language = "English",
                Publisher = "Penguin",
                ISBN10 = "1928374655"
            };

            var repoMock = automocker.GetMock<IRepository<Book>>();
            repoMock
                .Setup(x => x.AddAsync(It.IsAny<Book>()))
                .ReturnsAsync(new Book());

            // Act
            await sut.Handle(command, CancellationToken.None);

            // Assert
            var entity = repoMock.Invocations[0].Arguments[0] as Book;
            entity.Author.ShouldBe("karen deeney");
        }
    }
}