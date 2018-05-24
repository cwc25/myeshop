using System;
using System.Threading.Tasks;
using EventBus;
using Order.API.IntegrationEvents.Events;
using Order.API.Model;
using Order.API.Repository;

namespace Order.API.IntegrationEvents.EventsHandler
{
    public class UserCheckoutAcceptedIntegrationEventHandler: IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>
    {
        private IOrderIntegrationEventService _orderIntegrationEventService;
		private IOrderQueries _orderQueries;

        public UserCheckoutAcceptedIntegrationEventHandler(IOrderIntegrationEventService orderIntegrationEventService,
		                                                   IOrderQueries orderQueries)
        {
            _orderIntegrationEventService = orderIntegrationEventService;
			_orderQueries = orderQueries;
        }
        
        public async Task Handle(UserCheckoutAcceptedIntegrationEvent @event)
        {
			Console.WriteLine("Order - user checkout handler started");
            //delete basket since it become order by sending orderstared event and handled by orderstart handler in basket project.
            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent("1");
            await _orderIntegrationEventService.PublishThroughEventBusAsync(orderStartedIntegrationEvent);
			var order = new OrderDto()
			{
				Total = @event.Total
			};

		    _orderQueries.InsertOrderAsync(order);
        }
    }
}
