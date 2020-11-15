using Domain.Common;

namespace Domain.Events
{
    public class BookCreatedEvent : BaseEvent
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}