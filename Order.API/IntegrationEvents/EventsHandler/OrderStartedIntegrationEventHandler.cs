using System;
using System.Threading.Tasks;
using Order.API.Repository;
using EventBus;
using Order.API.IntegrationEvents.Events;

namespace Order.API.IntegrationEvents.EventsHandler
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
		//private readonly IOrderRepository _repository;
		private IOrderQueries _queries;

		public OrderStartedIntegrationEventHandler(IOrderQueries queries)
        {
			_queries = queries;
        }

        //user click checkout button to turn basket into order, so 1. remove basket from redis 2. insert data into mysql order table.
        public Task Handle(OrderStartedIntegrationEvent @event)
        {
        }
    }
}
