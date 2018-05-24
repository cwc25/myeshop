using System;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Order.API.Repository
{
    public class MongoDataRepository : IOrderRepository
    {
        private readonly MongoDataContext _context;

        public MongoDataRepository(IOptions<MongoSetting> settings)
        {
            _context = new MongoDataContext(settings);
        }

        public async Task<MongoCollectionDocument> GetAsync(string userid)
        {
            var filter = Builders<MongoCollectionDocument>.Filter.Eq("UserId", userid);

            return await _context.MongoCollection
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task UpdateItemAsync(MongoCollectionDocument mongoCollectionDocument)
        {
            var filter = Builders<MongoCollectionDocument>.Filter.Eq("UserId", mongoCollectionDocument.UserId);
            var update = Builders<MongoCollectionDocument>.Update
                .Set("Locations", mongoCollectionDocument.Location)
                .CurrentDate("UpdateDate");

            await _context.MongoCollection
                .UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }
    }
}
