using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.IntegrationEvents;
using Basket.API.IntegrationEvents.Events;
using Basket.API.Model;
using EventBus;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private IEventBus _eventBus;

        public BasketController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/checkout
        [Route("checkout")]
        [HttpPost]
		public void CheckOut([FromBody]BasketDto basket)
        {
            //to do - get basket info from redis

			UserCheckoutAcceptedIntegrationEvent @event = new UserCheckoutAcceptedIntegrationEvent(){ Total = basket.Total};
            _eventBus.Publish(@event);
        }

        // PUT api/values/5
        [HttpPost]
        public string TestPost([FromBody]string value)
        {
            return "value";
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
