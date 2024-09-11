using Newtonsoft.Json;

namespace CryptoCoinTracker.DataFetcher.Models;

public class Crypto
{
    [JsonProperty("bitcoin")]
    public CryptoData? Bitcoin { get; set; }

    [JsonProperty("solana")]
    public CryptoData? Solana { get; set; }

    [JsonProperty("ethereum")]
    public CryptoData? Ethereum { get; set; }

    [JsonProperty("binancecoin")]
    public CryptoData? BinanceCoin { get; set; }

    [JsonProperty("dogecoin")]
    public CryptoData? DogeCoin { get; set; }
}

public class CryptoData
{
    [JsonProperty("usd")]
    public decimal Usd { get; set; }

    [JsonProperty("usd_market_cap")]
    public decimal UsdMarketCap { get; set; }
}