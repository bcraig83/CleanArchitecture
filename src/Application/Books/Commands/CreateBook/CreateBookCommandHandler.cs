using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IRepository<Book> _repository;

        public CreateBookCommandHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(
            CreateBookCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Language = request.Language,
                Publisher = request.Publisher,
                ISBN10 = request.ISBN10
            };

            var result = await _repository.AddAsync(entity);

            return result.Id;
        }
    }
}