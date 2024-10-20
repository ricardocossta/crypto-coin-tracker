using RabbitMQ.Client;

namespace CryptoCoinTracker.Models.Settings;

public class BusConnection
{
    public IConnection Connection { get; set; }

    public BusConnection(IConnection connection)
    {
        Connection = connection;
    }
}
