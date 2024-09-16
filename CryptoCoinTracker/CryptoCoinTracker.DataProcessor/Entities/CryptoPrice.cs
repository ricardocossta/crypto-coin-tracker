using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CryptoCoinTracker.DataProcessor.Entities;

public class CryptoPrice
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("coin")]
    public string Coin { get; set; } = string.Empty;

    [BsonElement("usd")]
    public decimal Usd { get; set; }

    [BsonElement("usd_market_cap")]
    public decimal UsdMarketCap { get; set; }
}
