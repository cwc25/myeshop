using Basket.API.IntegrationEvents.Events;
using EventBus;
using Microsoft.eShopOnContainers.Services.Basket.API.Model;
using System;
using System.Threading.Tasks;

namespace Basket.API.IntegrationEvents.EventHandling
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;

      /*  public OrderStartedIntegrationEventHandler(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
      */  
		public OrderStartedIntegrationEventHandler()
		{
			
		}

        public async Task Handle(OrderStartedIntegrationEvent @event)
        {
            Console.WriteLine("basket - order started handler");

            await Task.CompletedTask;
            //await _repository.DeleteBasketAsync(@event.UserId.ToString());

        }
    }
}



