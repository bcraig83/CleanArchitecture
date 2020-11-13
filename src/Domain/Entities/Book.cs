using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public Format Format { get; set; }
        public string Publisher { get; set; }

        // e.g. 0321503627
        public string ISBN10 { get; set; }
    }
}