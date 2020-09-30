using Domain.Common;
using Domain.Enums;
using Domain.Events;
using System;

namespace Domain.Entities
{
    public class TodoItem : BaseEntity
    {
        public int ListId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public DateTime? Reminder { get; set; }

        public PriorityLevel Priority { get; set; }

        public TodoList List { get; set; }

        public bool IsDone { get; private set; } = false;

        public void MarkComplete()
        {
            IsDone = true;

            Events.Add(new ToDoItemCompletedEvent(this));
        }
    }
}