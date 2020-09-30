using Application.Common.Interfaces;
using Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Handlers
{
    public class ToDoItemCompletedEventEmailHandler : IBaseEventHandler<ToDoItemCompletedEvent>
    {
        public async Task Handle(
            ToDoItemCompletedEvent notification,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}