using System;
namespace Order.API.Model
{
    public class OrderDto
    {
        public OrderDto()
        {
        }

		public string OrderId { get; set; }
		public decimal Total { get; set; }
    }
}
