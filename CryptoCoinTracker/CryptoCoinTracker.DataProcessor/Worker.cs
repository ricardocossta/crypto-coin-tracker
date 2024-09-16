using CryptoCoinTracker.DataProcessor.Interface;

namespace CryptoCoinTracker.DataProcessor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMessageBusClient _messageBusClient;

    public Worker(ILogger<Worker> logger, IMessageBusClient messageBusClient)
    {
        _logger = logger;
        _messageBusClient = messageBusClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            _messageBusClient.Consume();

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}
