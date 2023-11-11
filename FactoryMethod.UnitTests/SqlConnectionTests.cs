using Moq;
using Xunit;

namespace FactoryMethod.UnitTests;


public class SqlConnectionTests
{    
    const string SQL_CONNECT_STRING = "DB_SQL://sql.url";
    const string SQL_COMMAND = "Insert command string";
    SqlConnection sqlConnection;
    Mock<ILogger> loggerMock;

    public SqlConnectionTests()
    {
        loggerMock = new Mock<ILogger>();
        sqlConnection = new SqlConnection(loggerMock.Object);

    }

    [Fact]
    public void Connect_ProvidedConnectionString_ChangesStateToConnected() {
        string expected = $"SQL was connected {SQL_CONNECT_STRING}";
        
        sqlConnection.Connect(SQL_CONNECT_STRING);
        string actual = sqlConnection.State;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Connect_NotProvidedConnectionString_ThrowArgumentException() {       
        Assert.Throws<ArgumentException>(() => sqlConnection.Connect(null));
    }

    [Fact]
    public void Execute_ExecuteOperation_ReturnsNunberOfAffectedRows() {
        sqlConnection.Connect(SQL_CONNECT_STRING);
        int expected = 1;

        int actual = sqlConnection.Execute(SQL_COMMAND);       

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Execute_SqlCommand_WriteToLog() {
        sqlConnection.Connect(SQL_CONNECT_STRING);
         
        sqlConnection.Execute(SQL_COMMAND);

        loggerMock.Verify(l => l.LogDebug(SQL_COMMAND));        
    }
}
