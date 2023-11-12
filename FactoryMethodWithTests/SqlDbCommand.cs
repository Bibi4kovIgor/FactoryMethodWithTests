
using System.Reflection;

namespace FactoryMethod;

public class SqlDbCommand<T, Key> : IDbCommand<T, Key> where T : new()
{
    private IConnection connection;

    public virtual string GetKey { get; } = "id";

    public SqlDbCommand(IConnection connection)
    {
        this.connection = connection;
    }    

    public bool Delete(Key key)
    {
        return connection.Execute($"Delete from {GetTableName()} where {GetKey} = {key} ") > 0;
    }

    public Task<T> GetAsync(Key key) 
    {
        return connection.ExecuteAsync<T>($"Select * from {GetTableName()} where {GetKey} = {key} ");
    }

    public List<T> GetAll()
    {
        return connection.Execute<List<T>>($"Select * from {GetTableName()}");        
    }

    public int Insert(T data)
    {
        return connection.Execute($"Insert {GetTableName()} {GetInsertColumns(data)}");                
    }

    public bool Update(Key key, T newData)
    {
        return connection.Execute($"Update {GetTableName()} SET{ForUpdateColumns(newData)} where {GetKey} = {GetValue(key)}") > 0;                
    }

    protected string GetTableName() {
        return typeof(T).Name; 
    }

    protected string GetInsertColumns(T data)
    {
        Type type = typeof(T);
        var columns = type.GetProperties().Where(p => p.Name != GetKey);
        return $"({string.Join(',', columns.Select(p => p.Name))}) values ({string.Join(',', columns.Select(p => p.GetValue(data)))})";
    }
    protected string ForUpdateColumns(T data)
    {
        Type type = typeof(T);
        var columns = type.GetProperties().Where(p => p.Name != GetKey);
        return $"{string.Join(',', columns.Select(p => $" {p.Name} = {GetValue(data, p)}"))}";
    }

    protected static object? GetValue(T data, PropertyInfo p)
    {
        return p.PropertyType == typeof(Guid) 
            || p.PropertyType == typeof(string)
            ? $"'{p.GetValue(data)}'" : p.GetValue(data);
    }
    protected static object? GetValue(Key key)
    {
        return typeof(Key) == typeof(Guid)
            || typeof(Key) == typeof(string)
            ? $"'{key}'": key;
    }
}
