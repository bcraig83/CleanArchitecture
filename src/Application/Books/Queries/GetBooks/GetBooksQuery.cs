using Application.Books.Queries.GetBooks.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Books.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<IList<BookDto>>
    {
    }
}