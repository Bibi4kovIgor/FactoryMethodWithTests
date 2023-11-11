namespace FactoryMethod;
public interface IConnection
{       
    void Connect(string connectionString);
    void Disconnect();
    int Execute(string command);
    T Execute<T>(string command);
    Task<T> ExecuteAsync<T>(string command) where T : new();
}

