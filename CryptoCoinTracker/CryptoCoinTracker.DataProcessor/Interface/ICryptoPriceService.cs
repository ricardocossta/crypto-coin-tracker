namespace CryptoCoinTracker.DataProcessor.Interface;

public interface ICryptoPriceService
{
    Task ProcessMessageAsync(string message);
}
