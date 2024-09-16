using CryptoCoinTracker.DataProcessor.Interface;
using CryptoCoinTracker.DataProcessor.Models.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CryptoCoinTracker.DataProcessor.MessageBus;

public class RabbitMqClient : IMessageBusClient
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMqClient> _logger;
    private readonly ICryptoPriceService _cryptoPriceService;

    public RabbitMqClient(ConsumerConnection consumerConnection, ILogger<RabbitMqClient> logger, ICryptoPriceService cryptoPriceService)
    {
        _logger = logger;

        _connection = consumerConnection.Connection;
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare("crypto_exchange", ExchangeType.Direct);
        _channel.QueueDeclare("crypto_data_queue", true, false, false, null);
        _channel.QueueBind("crypto_data_queue", "crypto_exchange", "crypto_key");
        _cryptoPriceService = cryptoPriceService;
    }

    public void Consume()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(contentArray);

            _logger.LogInformation("Received message: {message}", message);

            try
            {
                await _cryptoPriceService.ProcessMessageAsync(message);

                _logger.LogInformation("Message processed at: {time}", DateTimeOffset.Now);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message: {message}", message);
            }
        };

        _channel.BasicConsume("crypto_data_queue", false, consumer);
    }
}
