using Application.Common.Interfaces;
using Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Handlers
{
    public class ToDoItemCompletedEventEmailHandler : IBaseEventHandler<ToDoItemCompletedEvent>
    {
        private readonly IEmailSender _emailSender;

        public ToDoItemCompletedEventEmailHandler(
            IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(
            ToDoItemCompletedEvent notification,
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