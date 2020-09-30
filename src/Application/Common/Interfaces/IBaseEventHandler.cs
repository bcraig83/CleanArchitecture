using Domain.Common;
using MediatR;

namespace Application.Common.Interfaces
{
    public interface IBaseEventHandler<T> : INotificationHandler<T> where T : BaseEvent
    {
    }
}