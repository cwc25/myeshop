using System;
namespace Order.API.Repository
{
    public class MongoCollectionDocument
    {
        public MongoCollectionDocument()
        {
        }

        public string UserId { get; set; }
        public string Location { get; set; }
    }
}
