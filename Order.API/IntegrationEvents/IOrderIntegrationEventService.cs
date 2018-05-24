using System;
using System.Threading.Tasks;
using EventBus;
namespace Order.API.IntegrationEvents
{
    public interface IOrderIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent @event);
    }
}
