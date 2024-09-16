using CryptoCoinTracker.DataProcessor.Data;
using CryptoCoinTracker.DataProcessor.Entities;
using CryptoCoinTracker.DataProcessor.Interface;
using MongoDB.Driver;

namespace CryptoCoinTracker.DataProcessor.Repositories;

public class CryptoPriceRepository : ICryptoPriceRepository
{
    private readonly IMongoCollection<CryptoPrice> _cryptoCollection;

    public CryptoPriceRepository(MongoDbContext mongoDbContext)
    {
        var database = mongoDbContext.Database;
        _cryptoCollection = database.GetCollection<CryptoPrice>("CryptoPrices");
    }

    public async Task UpsertAsync(CryptoPrice cryptoPrice)
    {
        var filter = Builders<CryptoPrice>.Filter.Eq("coin", cryptoPrice.Coin);
        var update = Builders<CryptoPrice>.Update
            .Set("usd", cryptoPrice.Usd)
            .Set("usd_market_cap", cryptoPrice.UsdMarketCap);

        await _cryptoCollection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    }
}
