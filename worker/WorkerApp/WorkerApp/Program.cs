using WorkerApp;
using WorkerApp.Workers;

var builder = Host.CreateApplicationBuilder(args);

var kafkaConfig = new KafkaConfig();
builder.Configuration.GetSection("Kafka").Bind(kafkaConfig);

builder.Services
    .AddSingleton(kafkaConfig);

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole());

foreach (var kafkaTopic in kafkaConfig.Topics)
{
    switch (kafkaTopic.Topic)
    {
        case "Fruits":
            builder.Services.AddSingleton(s =>
            {
                var logger = loggerFactory.CreateLogger<FruitsWorker>();
                return Activator.CreateInstance(typeof(FruitsWorker), logger, kafkaConfig.BootstrapServers, kafkaTopic) as IHostedService ??
                    throw new ArgumentNullException();
            });
            break;
        case "Movies":
            builder.Services.AddSingleton(s =>
            {
                var logger = loggerFactory.CreateLogger<MoviesWorker>();
                return Activator.CreateInstance(typeof(MoviesWorker), logger, kafkaConfig.BootstrapServers, kafkaTopic) as IHostedService ??
                    throw new ArgumentNullException();
            });
            break;
        case "Pets":
            builder.Services.AddSingleton(s =>
            {
                var logger = loggerFactory.CreateLogger<PetsWorker>();
                return Activator.CreateInstance(typeof(PetsWorker), logger, kafkaConfig.BootstrapServers, kafkaTopic) as IHostedService ??
                    throw new ArgumentNullException();
            });
            break;
        default:
            break;
    }
}
    

var host = builder.Build();
host.Run();
