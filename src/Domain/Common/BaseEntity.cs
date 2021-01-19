using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        // TODO: should this setter be private?
        public int Id { get; set; }

        public List<BaseEvent> Events = new List<BaseEvent>();

        //public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        //public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}