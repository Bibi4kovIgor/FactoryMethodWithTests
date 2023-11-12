namespace FactoryMethod
{
    public class MongoDbFactory : DatabaseConnectionFactory
    {
        readonly ILogger logger;

        public MongoDbFactory(ILogger logger)
        {
            this.logger = logger;
        }

        public override IConnection CreateConnection()
        {
            return new MongoDBConnection(logger);
        }
    }
}
