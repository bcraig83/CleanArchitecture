using Domain.Entities;
using Domain.Events;

namespace Application.Books.Commands.CreateBook
{
    public class BookMapper
    {
        private readonly ApplicationOptions _options;

        public BookMapper(ApplicationOptions options)
        {
            _options = options;
        }

        public Book Map(CreateBookCommand command)
        {
            // TODO: handle _options == null
            // TODO: add unit tests around this
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