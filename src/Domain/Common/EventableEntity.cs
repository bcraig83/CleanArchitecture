using System.Collections.Generic;

namespace Domain.Common
{
    // TODO: come up with a better name here
    public abstract class EventableEntity : AuditableEntity
    {
        public int Id { get; set; }

        public List<BaseEvent> Events = new List<BaseEvent>();
    }
}