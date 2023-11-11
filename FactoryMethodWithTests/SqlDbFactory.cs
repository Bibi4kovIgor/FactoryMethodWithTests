namespace FactoryMethod
{
    public class SqlDbFactory : DatabaseConnectionFactory
    {
        ILogger logger;

        public SqlDbFactory(ILogger logger)
        {
            this.logger = logger;
        }

        public override IConnection CreateConnection()
        {
            return new SqlConnection(logger);
        }
    }
}
