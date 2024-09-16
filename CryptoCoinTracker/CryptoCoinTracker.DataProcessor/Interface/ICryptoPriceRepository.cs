using CryptoCoinTracker.DataProcessor.Entities;

namespace CryptoCoinTracker.DataProcessor.Interface;

public interface ICryptoPriceRepository
{
    Task UpsertAsync(CryptoPrice crypto);
}
