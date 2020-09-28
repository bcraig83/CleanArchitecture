using Application.TodoLists.Queries.ExportTodos.Models;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
