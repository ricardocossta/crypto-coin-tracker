using RabbitMQ.Client;

namespace CryptoCoinTracker.DataProcessor.Models.Settings;

public class ConsumerConnection
{
    public IConnection Connection { get; set; }
    public ConsumerConnection(IConnection connection)
    {
        Connection = connection;
    }
}
