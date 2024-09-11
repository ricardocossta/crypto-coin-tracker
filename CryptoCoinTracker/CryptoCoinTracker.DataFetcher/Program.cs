using CryptoCoinTracker.DataFetcher;
using CryptoCoinTracker.DataFetcher.Interfaces;
using CryptoCoinTracker.DataFetcher.Models.Settings;
using CryptoCoinTracker.DataFetcher.Service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<IDataCollectorService, DataCollectorService>();

builder.Services.AddHttpClient();

builder.Services.Configure<CoinGeckoApiSettings>(builder.Configuration.GetSection("CoinGeckoApi"));

var host = builder.Build();
host.Run();
