using Kitchen.Service.DataAccess.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kitchen.Service.DataAccess.Abstract
{
    public abstract class AbstractMongoDbRepository
    {
        protected IMongoDatabase Database { get; }

        protected AbstractMongoDbRepository(IOptions<DatabaseSettings> mongoDbSettings)
        {
            var settings = mongoDbSettings.Value;

            var mongoClient = new MongoClient(settings.ConnectionString);
            Database = mongoClient.GetDatabase(settings.DatabaseName);
        }

        protected IMongoCollection<T> GetCollection<T>(Collections collectionName)
        {
            return Database.GetCollection<T>(collectionName.ToString());
        }
    }
}
