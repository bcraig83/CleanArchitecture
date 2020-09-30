using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class TodoList : BaseEntity
    {
        public TodoList()
        {
            Items = new List<TodoItem>();
        }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItem> Items { get; set; }
    }
}
