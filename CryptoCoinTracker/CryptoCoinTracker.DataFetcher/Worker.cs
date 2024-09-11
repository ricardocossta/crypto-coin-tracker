using CryptoCoinTracker.DataFetcher.Interfaces;

namespace CryptoCoinTracker.DataFetcher
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDataCollectorService _dataCollectorService;

        public Worker(ILogger<Worker> logger, IDataCollectorService dataCollectorService)
        {
            _logger = logger;
            _dataCollectorService = dataCollectorService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var response = await _dataCollectorService.FetchDataAsync();

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
