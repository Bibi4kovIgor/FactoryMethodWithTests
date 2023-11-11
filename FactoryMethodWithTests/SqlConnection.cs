namespace FactoryMethod;

public class SqlConnection : IConnection
{
    private ILogger logger;

    private const string CONNECTION_MESSAGE = "SQL was connected {0}";
    private const string DISCONNECT_MESSAGE = "SQL was disconnected";
    private const string CONNECTION_ERROR = "Connection unavailable!";

    public string State { get; set; }

    public SqlConnection(ILogger logger)
    {
        this.logger = logger;
    }

    public void Connect(string connectionString)
    { 
        if(connectionString == null) { throw new ArgumentException("Connection string is empty", nameof(connectionString)); }
        State = string.Format(CONNECTION_MESSAGE, connectionString);
        logger.LogDebug(State);       
    }

    public void Disconnect()
    {
        State = DISCONNECT_MESSAGE;
        logger.LogDebug(State);
    }

    public int Execute(string command)
    {
        ThrowIfNotConnected();
        logger.LogDebug(command);
        return 1;
    }


    public T? Execute<T>(string command)
    {
        ThrowIfNotConnected();
        logger.LogDebug(command);
        return default;
    }
    private void ThrowIfNotConnected()
    {
        if (State == DISCONNECT_MESSAGE || State == null) { throw new InvalidOperationException(CONNECTION_ERROR); }
    }
    public Task<T> ExecuteAsync<T>(string command) where T : new()
    {
        logger.LogDebug(command);
        return Task.FromResult(new T());
    }
}
