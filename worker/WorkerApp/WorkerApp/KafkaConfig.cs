namespace WorkerApp
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; } = null!;
        public List<KafkaTopic> Topics { get; set; } = [];
    }

    public class KafkaTopic
    {
        public string Topic { get; set; } = null!;
        public string ConsumerGroup { get; set; } = null!;
    }
}
