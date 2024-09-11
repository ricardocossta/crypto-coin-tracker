using CryptoCoinTracker.DataFetcher.Models;

namespace CryptoCoinTracker.DataFetcher.Interfaces;

public interface IDataCollectorService
{
    Task<Crypto?> FetchDataAsync();
}
