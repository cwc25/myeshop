using System;
using EventBus;

namespace Basket.API.IntegrationEvents.EventsHandler
{
    public class UserCheckoutAcceptedIntegrationEventHandler
    {
        public UserCheckoutAcceptedIntegrationEventHandler()
        {
        }

        public void Handle(IntegrationEvent @event)
        {
            // Send Integration event to clean basket once basket is converted to Order and before starting with the order creation process

        }
    }
}
