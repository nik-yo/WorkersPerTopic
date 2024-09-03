using Confluent.Kafka;

namespace WorkerApp
{
    public class Worker(ILogger<Worker> logger, string bootstrapServers, KafkaTopic kafkaTopic) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = bootstrapServers,
                GroupId = kafkaTopic.ConsumerGroup,
                AllowAutoCreateTopics = true,
                SecurityProtocol = SecurityProtocol.Plaintext
            };

            using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
            {
                consumer.Subscribe(kafkaTopic.Topic);
                try
                {
                    var childStoppingToken = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

                    await Task.Run(async () => await StartPolling(consumer, childStoppingToken.Token));
                }
                catch (Exception ex)
                {
                    // Ctrl-C was pressed.
                    logger.LogError(ex, ex.Message);
                }
                //finally
                //{
                //    consumer.Close();
                //}

                return;
            }
        }

        public Task StartPolling(IConsumer<string, string> consumer, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(stoppingToken);
                if (consumeResult.Message != null)
                {
                    Console.WriteLine($"{GetPrefix()}: {consumeResult.Message.Value}");
                }
            }

            return Task.CompletedTask;
        }

        public virtual string GetPrefix()
        {
            return "Worker";
        } 
    }
}
