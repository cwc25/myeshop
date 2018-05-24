using System;
using EventBus;

namespace Basket.API.IntegrationEvents.Events
{
    public class UserCheckoutAcceptedIntegrationEvent: IntegrationEvent
    {
        public UserCheckoutAcceptedIntegrationEvent()
        {
        }

		public decimal Total { get; set; }
    }
}
