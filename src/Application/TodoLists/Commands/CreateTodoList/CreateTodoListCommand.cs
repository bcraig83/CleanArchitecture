using MediatR;

namespace Application.TodoLists.Commands.CreateTodoList
{
    public partial class CreateTodoListCommand : IRequest<int>
    {
        public string Title { get; set; }
    }
}