namespace CryptoCoinTracker.DataFetcher.Models.Settings;

public class CoinGeckoApiSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string CoinPriceByIdEndpoint { get; set; } = string.Empty;
}
