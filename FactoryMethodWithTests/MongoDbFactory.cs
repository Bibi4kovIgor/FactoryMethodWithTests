namespace FactoryMethod
{
    public class MongoDbFactory : DatabaseConnectionFactory
    {
        public override IConnection CreateConnection()
        {
            return new MongoDBConnection();
        }
    }
}
