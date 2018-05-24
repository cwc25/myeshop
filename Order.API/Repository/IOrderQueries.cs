using System;
using System.Threading.Tasks;
using Order.API.Model;

namespace Order.API.Repository
{
    public interface IOrderQueries
    {
	    Task<OrderDto> GetOrderAsync(int id);
		void InsertOrderAsync(OrderDto order);
    }
}
