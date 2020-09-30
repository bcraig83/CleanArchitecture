using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class TodoItemCompletedEvent : BaseEvent
    {
        public TodoItem CompletedItem { get; set; }

        public TodoItemCompletedEvent(TodoItem completedItem)
        {
            CompletedItem = completedItem;
        }
    }
}