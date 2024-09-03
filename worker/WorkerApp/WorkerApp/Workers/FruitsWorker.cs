namespace WorkerApp.Workers
{
    public class FruitsWorker : Worker
    {
        public FruitsWorker(ILogger<FruitsWorker> logger, string bootstrapServers, KafkaTopic kafkaTopic) : base(logger, bootstrapServers, kafkaTopic)
        {
        }

        public override string GetPrefix()
        {
            return "Fruits";
        }
    }
}
