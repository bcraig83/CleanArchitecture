using Domain.Common;
using MediatR;
using System.Threading.Tasks;

namespace DataAccess.InMemory
{
    public class EventProcessor
    {
        private readonly IMediator _mediator;

        public EventProcessor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ProcessEvents(BaseEntity entity)
        {
            var events = entity.Events.ToArray();
            entity.Events.Clear();

            foreach (var domainEvent in events)
            {
                await _mediator
                    .Publish(domainEvent)
                    .ConfigureAwait(false);
            }
        }
    }
}