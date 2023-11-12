using Moq;
using Xunit;
using Xunit.Abstractions;

namespace FactoryMethod.UnitTests;

public class IntegrationalTest
{
    private class UserTable
    {
        public string Name { get; set; }
        public int Id { get; set; }

    }

    private readonly ITestOutputHelper output;

    public IntegrationalTest(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public async Task IntegrationalTest_SystemCheckSummary()
    {
        var userTable =  new UserTable { Id = 1, Name = "Test" };
        var userTable3 =  new UserTable { Id = 3, Name = "Test3" };

        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.LogDebug(It.IsAny<string>())).Callback((string s) => output.WriteLine(s));

        DatabaseConnectionFactory factory = new SqlDbFactory(mockLogger.Object);
        IConnection connection = factory.CreateConnection();
        connection.Connect("Connection String");

        output.WriteLine("Connection established");

        var sqlCommand = new SqlDbCommand<UserTable, int>(connection);
        sqlCommand.Insert(userTable);      
        sqlCommand.Update(5, userTable3);
        sqlCommand.Delete(1);
        sqlCommand.GetAll();
        await sqlCommand.GetAsync(1);
    }
}

