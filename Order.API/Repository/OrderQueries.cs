using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using Dapper;
using Order.API.Model;

namespace Order.API.Repository
{
	public class OrderQueries: IOrderQueries
    {
		private string _connectionString = String.Empty; //"server=mysql;user=root;database=eshop;port=3306;password=password";

        public OrderQueries(string connectionString)
        {
			_connectionString = connectionString;
        }

		public Task<OrderDto> GetOrderAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async void InsertOrderAsync(OrderDto order)
		{
			Console.WriteLine("OrderQueries Insert Order");
			//string connStr = "server=localhost;user=root;database=world;port=3306;password=******";
			using(var conn = new MySqlConnection(_connectionString))
			{
				string query = "INSERT INTO eshop.order(total) VALUES(@Total)";
				int rowsAffected = await conn.ExecuteAsync(query, order);
			}
		}
	}
}
