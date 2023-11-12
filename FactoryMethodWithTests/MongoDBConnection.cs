namespace FactoryMethod
{
    internal class MongoDBConnection : IConnection       
    {
        private readonly ILogger logger;

        public MongoDBConnection(ILogger logger)
        {
            this.logger = logger;
        }

        public void Connect(string connectionString) => Console.WriteLine($"MongoDB was connected {connectionString}");
        public void Disconnect() => Console.WriteLine("MongoDB was disconnected");
        public int Execute(string command)
        {
            logger.LogDebug(command);
            return 1;
        }

        public T? Execute<T>(string command)
        {
            logger.LogDebug(command);
            return default;
        }

        public Task<T> ExecuteAsync<T>(string command) where T : new()
        {
            logger.LogDebug(command);
            return Task.FromResult(new T());
        }
    }
}
