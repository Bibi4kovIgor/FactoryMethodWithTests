namespace FactoryMethod
{
    public interface IDbOperations
    {
        void CreateTable(String tableName, IConnection connection);
        void DeleteTable(String tableName, IConnection connection);        
    }
}