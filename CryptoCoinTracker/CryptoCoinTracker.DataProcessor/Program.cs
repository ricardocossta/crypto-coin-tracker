using CryptoCoinTracker.DataProcessor;
using CryptoCoinTracker.DataProcessor.Data;
using CryptoCoinTracker.DataProcessor.Interface;
using CryptoCoinTracker.DataProcessor.MessageBus;
using CryptoCoinTracker.DataProcessor.Models.Settings;
using CryptoCoinTracker.DataProcessor.Repositories;
using CryptoCoinTracker.DataProcessor.Services;
using CryptoCoinTracker.Models.Settings;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddTransient<ICryptoPriceRepository, CryptoPriceRepository>();
builder.Services.AddTransient<ICryptoPriceService, CryptoPriceService>();

//Mongo
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbContext>();

//RabbitMq
var connectionFactory = new ConnectionFactory { HostName = "localhost", };
var connection = connectionFactory.CreateConnection();
builder.Services.AddSingleton(new BusConnection(connection));

builder.Services.AddSingleton<IMessageBusClient, RabbitMqClient>();

var host = builder.Build();
host.Run();
