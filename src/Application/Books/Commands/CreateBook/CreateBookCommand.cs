using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Publisher { get; set; }
        public string ISBN10 { get; set; }

        // TODO: add format
    }
}