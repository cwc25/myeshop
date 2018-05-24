using System;
using EventBus;
namespace Order.API.IntegrationEvents.Events
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public OrderStartedIntegrationEvent(string userId)
        {
        }
    }
}
