using CryptoCoinTracker.DataFetcher;
using CryptoCoinTracker.DataFetcher.Interfaces;
using CryptoCoinTracker.DataFetcher.MessageBus;
using CryptoCoinTracker.DataFetcher.Models.Settings;
using CryptoCoinTracker.DataFetcher.Service;
using CryptoCoinTracker.Models.Settings;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<IDataCollectorService, DataCollectorService>();

builder.Services.AddHttpClient();

builder.Services.Configure<CoinGeckoApiSettings>(builder.Configuration.GetSection("CoinGeckoApi"));

//RabbitMq
var connectionFactory = new ConnectionFactory { HostName = "rabbitmq", Port = 5672 };
var connection = connectionFactory.CreateConnection();
builder.Services.AddSingleton(new BusConnection(connection));

builder.Services.AddSingleton<IMessageBusClient, RabbitMqClient>();

var host = builder.Build();
host.Run();
