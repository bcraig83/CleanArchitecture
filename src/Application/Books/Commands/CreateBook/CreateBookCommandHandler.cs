using Domain.Entities;
using Domain.Events;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IRepository<Book> _repository;
        private readonly ApplicationOptions _options;

        public CreateBookCommandHandler(
            IRepository<Book> repository,
            ApplicationOptions options)
        {
            _repository = repository;
            _options = options;
        }

        public async Task<int> Handle(
            CreateBookCommand request,
            CancellationToken cancellationToken)
        {
            // TODO: handle _options == null
            // TODO: add unit tests around this
            string author = _options.StoreAuthorInLowercase 
                ? request?.Author?.ToLower() 
                : request?.Author;

            var entity = new Book
            {
                Title = request.Title,
                Author = author,
                Language = request.Language,
                Publisher = request.Publisher,
                ISBN10 = request.ISBN10
            };

            var @event = new BookCreatedEvent
            {
                Title = entity.Title,
                Author = entity.Author
            };

            entity.Events.Add(@event);

            var result = await _repository.AddAsync(entity);

            return result.Id;
        }
    }
}