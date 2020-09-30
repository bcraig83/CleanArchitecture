using Application.Common.Interfaces;
using Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Handlers
{
    public class TodoItemCompletedEventEmailHandler : IBaseEventHandler<TodoItemCompletedEvent>
    {
        // This is just a sample. I wouldn't recommend sending an email on every completed TodoItem!
        private readonly IEmailSender _emailSender;

        public TodoItemCompletedEventEmailHandler(
            IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(
            TodoItemCompletedEvent notification,
            CancellationToken cancellationToken)
        {
            await _emailSender.SendEmailAsync(
                "test@test.com",
                "test@test.com",
                $"{notification.CompletedItem.Title} was completed.",
                notification.CompletedItem.ToString());
        }
    }
}