namespace WorkerApp.Workers
{
    public class MoviesWorker : Worker
    {
        public MoviesWorker(ILogger<MoviesWorker> logger, string bootstrapServers, KafkaTopic kafkaTopic) : base(logger, bootstrapServers, kafkaTopic)
        {
        }

        public override string GetPrefix()
        {
            return "Movies";
        }
    }
}
