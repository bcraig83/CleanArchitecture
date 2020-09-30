using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ToDoItemCompletedEvent : BaseEvent
    {
        public TodoItem CompletedItem { get; set; }

        public ToDoItemCompletedEvent(TodoItem completedItem)
        {
            CompletedItem = completedItem;
        }
    }
}