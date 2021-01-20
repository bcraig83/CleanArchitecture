using Application.Books.Commands.CreateBook.Services;
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
        private readonly BookMapper _mapper;

        public CreateBookCommandHandler(
            IRepository<Book> repository,
            BookMapper mapper)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(
            CreateBookCommand request,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map(request);
            var result = await _repository.AddAsync(entity);
            return result.Id;
        }
    }
}