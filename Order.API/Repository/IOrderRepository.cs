using System;
using System.Threading.Tasks;

namespace Order.API.Repository
{
    public interface IOrderRepository
    {
        Task<MongoCollectionDocument> GetAsync(string userid);
        Task UpdateItemAsync(MongoCollectionDocument mongoCollectionDocument);
    }
}
