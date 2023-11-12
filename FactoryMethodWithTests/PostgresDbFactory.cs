namespace FactoryMethod
{
    public class PostgresDbFactory : DatabaseConnectionFactory
    {
        readonly ILogger logger;

        public PostgresDbFactory(ILogger logger)
        {
            this.logger = logger;
        }

        public override IConnection CreateConnection()
        {
            return new PostgresConnection(logger);
        }
    }
}
