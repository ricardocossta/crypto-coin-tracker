using CryptoCoinTracker.DataFetcher.Extensions;
using CryptoCoinTracker.DataFetcher.Interfaces;
using CryptoCoinTracker.DataFetcher.Models;
using CryptoCoinTracker.DataFetcher.Models.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CryptoCoinTracker.DataFetcher.Service;

public class DataCollectorService : IDataCollectorService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly CoinGeckoApiSettings _apiSettings;

    public DataCollectorService(IHttpClientFactory httpClientFactory, IOptions<CoinGeckoApiSettings> config)
    {
        _httpClientFactory = httpClientFactory;
        _apiSettings = config.Value;
    }

    public async Task<Crypto?> FetchDataAsync()
    {
        var httpClient = _httpClientFactory.CreateClient().ConfigureCoinGeckoClient(_apiSettings);

        var response = await httpClient.GetStringAsync("simple/price?ids=solana,bitcoin,ethereum,binancecoin,dogecoin&vs_currencies=usd&include_market_cap=true");

        return JsonConvert.DeserializeObject<Crypto>(response);
    }
}
