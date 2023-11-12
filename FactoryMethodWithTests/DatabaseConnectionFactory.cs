namespace FactoryMethod
{
    public abstract class DatabaseConnectionFactory
    {
        public abstract IConnection CreateConnection();
    }
}
