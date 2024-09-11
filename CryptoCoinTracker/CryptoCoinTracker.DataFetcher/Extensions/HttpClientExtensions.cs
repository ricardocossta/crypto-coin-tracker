using CryptoCoinTracker.DataFetcher.Models.Settings;

namespace CryptoCoinTracker.DataFetcher.Extensions;

public static class HttpClientExtensions
{
    public static HttpClient ConfigureCoinGeckoClient(this HttpClient client, CoinGeckoApiSettings apiSettings)
    {
        client.BaseAddress = new Uri(apiSettings.BaseUrl);
        client.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiSettings.ApiKey);

        return client;
    }
}
