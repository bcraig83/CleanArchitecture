using Application.TodoLists.Queries.GetTodos.Models;
using MediatR;

namespace Application.TodoLists.Queries.GetTodos
{
    public class GetTodosQuery : IRequest<TodosVm>
    {
    }
}