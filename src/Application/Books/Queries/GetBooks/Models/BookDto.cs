using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Books.Queries.GetBooks.Models
{
    public class BookDto : IMapFrom<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        //public string Format { get; set; }
        public string Publisher { get; set; }
        public string ISBN10 { get; set; }
    }
}