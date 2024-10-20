using CryptoCoinTracker.DataFetcher.Interfaces;
using CryptoCoinTracker.Models.Settings;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CryptoCoinTracker.DataFetcher.MessageBus;

public class RabbitMqClient : IMessageBusClient
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMqClient> _logger;

    public RabbitMqClient(BusConnection busConnection, ILogger<RabbitMqClient> logger)
    {
        _logger = logger;
        _connection = busConnection.Connection;

        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare("crypto_exchange", ExchangeType.Direct);
        _channel.QueueDeclare("crypto_data_queue", true, false, false, null);
        _channel.QueueBind("crypto_data_queue", "crypto_exchange", "crypto_key");
    }

    public void Publish(object message)
    {
        var payload = JsonConvert.SerializeObject(message);
        var byteMessage = Encoding.UTF8.GetBytes(payload);

        _channel.BasicPublish("crypto_exchange", "crypto_key", null, byteMessage);

        _logger.LogInformation("Data published: {data} to RabbitMQ at: {time}", payload, DateTimeOffset.Now);
    }
}
