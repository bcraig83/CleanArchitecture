using Application.Books.Queries.GetBooks.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IList<BookDto>>
    {
        private readonly IRepository<Book> _repository;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(
            IRepository<Book> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<BookDto>> Handle(
            GetBooksQuery request,
            CancellationToken cancellationToken)
        {
            var result = new List<BookDto>();

            var booksInDatabase = await _repository.GetAllAsync();

            foreach (var book in booksInDatabase)
            {
                var mappedBook = _mapper.Map<BookDto>(book);
                result.Add(mappedBook);
            }

            return result;
        }
    }
}