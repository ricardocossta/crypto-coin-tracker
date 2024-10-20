using CryptoCoinTracker.Models;

namespace CryptoCoinTracker.DataFetcher.Interfaces;

public interface IDataCollectorService
{
    Task<Crypto> FetchDataAsync();
}
