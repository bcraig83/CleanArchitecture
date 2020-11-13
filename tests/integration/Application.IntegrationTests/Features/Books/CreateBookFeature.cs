﻿using Application.Books.Commands.CreateBook;
using Application.Common.Exceptions;
using Shouldly;
using System.Linq;
using Xunit;

namespace Application.IntegrationTests.Features.Books
{
    [Collection("Application test collection")]
    public class CreateBookFeature
    {
        private readonly ApplicationTestFixture _fixture;

        public CreateBookFeature(ApplicationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void ShouldThrowException_WhenTitleIsInvalid(string title)
        {
            // Arrange
            var command = new CreateBookCommand
            {
                Title = title
            };

            // Act
            var exception = (ValidationException)await Record.ExceptionAsync(async () =>
            {
                var result = await _fixture.SendAsync(command);
            });

            // Assert
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldContain("One or more validation failures have occurred.");

            var errors = exception.Errors;
            errors.ShouldNotBeNull();

            errors.TryGetValue("Title", out string[] errorText);
            errorText.ShouldNotBeNull();
            errorText.Count().ShouldBe(1);
            errorText[0].ShouldBe("Cannot create a book without a title");
        }

        [Fact]
        public async void ShouldThrowException_WhenIsbnIsInvalid()
        {
        }
    }
}