using CryptoCoinTracker.DataFetcher.Extensions;
using CryptoCoinTracker.DataFetcher.Interfaces;
using CryptoCoinTracker.DataFetcher.Models.Settings;
using CryptoCoinTracker.Models;
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

    public async Task<Crypto> FetchDataAsync()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient().ConfigureCoinGeckoClient(_apiSettings);

            var response = await httpClient.GetStringAsync(_apiSettings.CoinPriceByIdEndpoint);

            var cryptoDataResponse = JsonConvert.DeserializeObject<Crypto>(response);

            return cryptoDataResponse ?? throw new Exception("Failed to deserialize API response.");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("A network error occurred while fetching data.", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception("An error occurred while processing the API response.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching data.", ex);
        }
    }
}
