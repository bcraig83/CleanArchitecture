using Domain.Common;
using MediatR;

namespace Application.Common.Interfaces
{
    // Implement this interface as required under "SomeFunctionalty/EventHandlers"
    public interface IBaseEventHandler<T> : INotificationHandler<T> where T : BaseEvent
    {
    }
}