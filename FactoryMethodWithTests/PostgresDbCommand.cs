
using System.Reflection;

namespace FactoryMethod;

public class PostgresDbCommand<T, Key> : SqlDbCommand<T, Key> where T : new()
{
    private IConnection connection;

    public PostgresDbCommand(IConnection connection) : base(connection) => this.connection = connection;

}
