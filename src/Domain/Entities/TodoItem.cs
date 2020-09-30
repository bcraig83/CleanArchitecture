using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class TodoItem : EventableEntity
    {
        public int ListId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public bool Done { get; set; }

        public DateTime? Reminder { get; set; }

        public PriorityLevel Priority { get; set; }


        public TodoList List { get; set; }
    }
}
