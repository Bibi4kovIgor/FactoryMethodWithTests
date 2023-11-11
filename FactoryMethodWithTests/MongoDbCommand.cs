using System.Reflection;

namespace FactoryMethod;
public class MongoDbCommand<T, Key> : IDbCommand<T, Key> where T : new()
{
    private IConnection connection;

    public virtual string GetKey { get; } = "_id"; 

    public MongoDbCommand(IConnection connection)
    {
        this.connection = connection;
    }

    public bool Delete(Key key)
    {
        var filter = $"{{ \"{GetKey}\": {ToMongoValue(key)} }}";
        var command = $"db.{GetTableName()}.deleteOne({filter})";
        return connection.Execute(command) > 0;
        
    }

    public Task<T> GetAsync(Key key)
    {
        var filter = $"{{ \"{GetKey}\": {ToMongoValue(key)} }}";
        var command = $"db.{GetTableName()}.findOne({filter})";
        return connection.ExecuteAsync<T>(command);
    }

    public List<T> GetAll()
    {
        var command = $"db.{GetTableName()}.find().toArray()";
        return connection.Execute<List<T>>(command);
    }

    public int Insert(T data)
    {
        var command = $"db.{GetTableName()}.insertOne({ToMongoDocument(data)})";
        connection.Execute(command);
        return 1; 
    }

    public bool Update(Key key, T newData)
    {
        var filter = $"{{ \"{GetKey}\": {ToMongoValue(key)} }}";
        var update = $"{{ $set: {ToMongoDocument(newData)} }}";
        var command = $"db.{GetTableName()}.updateOne({filter}, {update})";
        return connection.Execute(command) > 0;
    }
    private string GetTableName()
    {
        return typeof(T).Name;
    }


    private string ToMongoValue(object value)
    {
        return value is string ? $"\"{value}\"" : value.ToString();
    }

    private string ToMongoDocument(T data)
    {
        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var keyValuePairs = properties
            .Where(p => p.Name != GetKey)
            .Select(p => $"\"{p.Name}\": {ToMongoValue(p.GetValue(data))}");

        return $"{{ {string.Join(", ", keyValuePairs)} }}";
    }
}

