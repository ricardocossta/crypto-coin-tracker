namespace CryptoCoinTracker.DataFetcher.Interfaces;

public interface IMessageBusClient
{
    void Publish(object message);
}
