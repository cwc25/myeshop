using System;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Order.API.Repository
{
    public class MongoDataContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDataContext(IOptions<MongoSetting> settings)
        {
            var client = new MongoClient(settings.Value.MongoConnectionString);

            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.MongoDatabase);
            }
        }

        public IMongoCollection<MongoCollectionDocument> MongoCollection
        {
            get
            {
                return _database.GetCollection<MongoCollectionDocument>("CollectionOne");
            }
        }
    }
}
