using System.Data;

namespace FactoryMethod;

public class PostgresConnection : IConnection
{
    private const string CONNECTION_STRING_ERROR = "Connections string postgresql was empty";
    private const string CONNECTION_ERROR = "Postgresql connection unavailable!";
    private const string POSTGRES_DISCONNECTED_MESSAGE = "Postgresql was disconnected";
    private const string POSTGRES_CONNECTED_MESSAGE = "Postgresql was connected {0}";

    readonly ILogger logger;
    public bool ConnectionState { get; set; } = false;

    public PostgresConnection(ILogger logger)
    {
        this.logger = logger;
    }

    public void Connect(string connectionString)
    {
        if(connectionString == null)
        {
            throw new ArgumentNullException(CONNECTION_STRING_ERROR);
        }

        ConnectionState = true;
        logger.LogDebug(string.Format(POSTGRES_CONNECTED_MESSAGE, connectionString));
    }

    public void Disconnect()
    {
        if (ConnectionState)
        {
            ConnectionState = false;
            logger.LogDebug(POSTGRES_DISCONNECTED_MESSAGE);
        }
    }

    public int Execute(string command)
    {
        ThrowIfNotConnected();
        Console.WriteLine(command);
        return 1;
    }

    public T? Execute<T>(string command)
    {
        ThrowIfNotConnected();
        Console.WriteLine(command);
        return default;
    }

    public Task<T> ExecuteAsync<T>(string command) where T : new() 
    {
        Console.WriteLine(command);
        return Task.FromResult(new T());
    }

    private void ThrowIfNotConnected()
    {
        if (!ConnectionState) { throw new InvalidOperationException(CONNECTION_ERROR); }
    }
}
