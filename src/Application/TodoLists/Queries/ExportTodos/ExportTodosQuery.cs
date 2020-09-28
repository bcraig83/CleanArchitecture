using Application.TodoLists.Queries.ExportTodos.Models;
using MediatR;

namespace Application.TodoLists.Queries.ExportTodos
{
    public class ExportTodosQuery : IRequest<ExportTodosVm>
    {
        public int ListId { get; set; }
    }
}