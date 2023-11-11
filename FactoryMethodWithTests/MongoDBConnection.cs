namespace FactoryMethod
{
    internal class MongoDBConnection : IConnection
    {
        public void Connect(string connectionString) => Console.WriteLine($"MongoDB was connected {connectionString}");
        public void Disconnect() => Console.WriteLine("MongoDB was disconnected");
        public int Execute(string command)
        {
            Console.WriteLine(command);
            return 1;
        }

        public T? Execute<T>(string command)
        {
            Console.WriteLine(command);
            return default;
        }

        public Task<T> ExecuteAsync<T>(string command) where T : new()
        {
            Console.WriteLine(command);
            return Task.FromResult(new T());
        }
    }
}
