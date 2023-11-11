using Moq;
using Xunit;
using Xunit.Abstractions;

namespace FactoryMethod.UnitTests;

public class UnitTest1
{
    private class UserTable
    {
        public string Name { get; set; }
        public int Id { get; set; }

    }

    private readonly ITestOutputHelper output;

    public UnitTest1(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
        public async Task IntegrationalTest_SystemCheckSummary()
    {
        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.LogDebug(It.IsAny<string>())).Callback((string s) => output.WriteLine(s));

        DatabaseConnectionFactory factory = new SqlDbFactory(mockLogger.Object);
        IConnection connection = factory.CreateConnection();
        connection.Connect("Connection String");

        output.WriteLine("Hello ");

        var sqlCommand = new SqlDbCommand<UserTable, int>(connection);
        sqlCommand.Insert(new UserTable { Id = 1, Name = "Test" });      
        sqlCommand.Update(5, new UserTable { Id = 3, Name = "Test3" });
        sqlCommand.Delete(1);
        sqlCommand.GetAll();
        await sqlCommand.GetAsync(1);
    }
}

