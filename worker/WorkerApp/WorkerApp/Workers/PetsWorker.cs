namespace WorkerApp.Workers
{
    public class PetsWorker : Worker
    {
        public PetsWorker(ILogger<PetsWorker> logger, string bootstrapServers, KafkaTopic kafkaTopic) : base(logger, bootstrapServers, kafkaTopic)
        {
        }

        public override string GetPrefix()
        {
            return "Pets";
        }
    }
}
