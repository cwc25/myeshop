using System;
using System.Threading.Tasks;
using EventBus;

namespace Order.API.IntegrationEvents
{
    public class OrderIntegrationEventService : IOrderIntegrationEventService
    {
        private readonly IEventBus _eventBus;

        public OrderIntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task PublishThroughEventBusAsync(IntegrationEvent @event)
        {
            //use event bus to pubish topic
            _eventBus.Publish(@event);

            return Task.CompletedTask;

        }
    }
}
