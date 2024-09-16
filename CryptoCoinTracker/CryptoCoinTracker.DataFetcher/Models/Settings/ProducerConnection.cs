using RabbitMQ.Client;

namespace CryptoCoinTracker.DataFetcher.Models.Settings;

public class ProducerConnection
{
    public IConnection Connection { get; set; }

    public ProducerConnection(IConnection connection)
    {
        Connection = connection;
    }
}
