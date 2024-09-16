using CryptoCoinTracker.DataFetcher.Interfaces;

namespace CryptoCoinTracker.DataFetcher;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IDataCollectorService _dataCollectorService;
    private readonly IMessageBusClient _messageBusClient;

    public Worker(ILogger<Worker> logger, IDataCollectorService dataCollectorService, IMessageBusClient messageBusClient)
    {
        _logger = logger;
        _dataCollectorService = dataCollectorService;
        _messageBusClient = messageBusClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var cryptoDataResponse = await _dataCollectorService.FetchDataAsync();

                _messageBusClient.Publish(cryptoDataResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching crypto data.");
            }

            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }
}
