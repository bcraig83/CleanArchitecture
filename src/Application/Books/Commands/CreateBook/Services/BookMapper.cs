using Domain.Entities;
using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Books.Commands.CreateBook.Services
{
    public class BookMapper
    {
        private readonly ApplicationOptions _options;

        public BookMapper(
            ILogger<BookMapper> logger,
            ApplicationOptions options)
        {
            logger.LogInformation($"Options: {_options}");

            _options = options ?? throw new System.ArgumentNullException(nameof(options));
        }

        public Book Map(CreateBookCommand command)
        {
            string author = _options.StoreAuthorInLowercase
                ? command?.Author?.ToLower()
                : command?.Author;

            var entity = new Book
            {
                Title = command.Title,
                Author = author,
                Language = command.Language,
                Publisher = command.Publisher,
                ISBN10 = command.ISBN10
            };

            var @event = new BookCreatedEvent
            {
                Title = entity.Title,
                Author = entity.Author
            };

            entity.Events.Add(@event);

            return entity;
        }
    }
}