using CryptoCoinTracker.DataProcessor.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CryptoCoinTracker.DataProcessor.Data;

public class MongoDbContext
{
    public IMongoDatabase Database { get; set; }

    public MongoDbContext(IOptions<DatabaseSettings> options)
    {
        var configuration = options.Value;
        var client = new MongoClient(configuration.ConnectionString);
        Database = client.GetDatabase(configuration.DatabaseName);
    }
}
