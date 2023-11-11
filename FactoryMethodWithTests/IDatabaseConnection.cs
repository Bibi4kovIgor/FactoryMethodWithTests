namespace FactoryMethod
{
    public interface IDatabaseConnection : IDisposable
    {
        void Connect(string connectionString);
        void Disconnect();
        IDbOperations CreateOperations();


    }
}
