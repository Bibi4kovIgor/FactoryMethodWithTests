using Moq;
using Xunit;

namespace FactoryMethod.UnitTests;

public class SqlDbCommandTest
{
    private record Address(Guid Uuid, string City, string Street) {
        public Address() : this(Guid.Empty, string.Empty, string.Empty) { }
    };

    const string STREET = "Haharyna Ave";
    const string CITY = "Kharkiv";
    static readonly Guid uuid = Guid.NewGuid();

    Mock<IConnection> connectionMock;

    public SqlDbCommandTest()
    {
        this.connectionMock = new Mock<IConnection>();
    }

    [Fact]
    public void Update_ExecuteUpdateCommand_UpdateTableRow() {
        // Arrange
        string expected = $"Update Address SET Uuid = '{uuid}', City = '{CITY}', Street = '{STREET}' where id = '{uuid}'";
        var sqldbCommand = new SqlDbCommand<Address, Guid>(connectionMock.Object);

        // Act
        sqldbCommand.Update(uuid, new Address(uuid, CITY, STREET));

        // Assert
        connectionMock.Verify(l => l.Execute(expected), Times.Once);        
    }

    [Fact]
    public void GetAll_GetAllDataFromTable_ReturnsListWithRows() {       
        connectionMock.Setup(c => c.Execute<List<Address>>(It.IsAny<string>()))
            .Returns(new List<Address> { new Address(uuid, CITY, STREET) });
        var expected = new List<Address> { new Address(uuid, CITY, STREET) };
        var sqldbCommand = new SqlDbCommand<Address, Guid>(connectionMock.Object);

        var actual = sqldbCommand.GetAll();

        Assert.Equal(expected, actual);        
    }

    [Fact]
    public async Task Get_GetDataFromTableByKey_ReturnsRow() {       
        connectionMock.Setup(c => c.ExecuteAsync<Address>(It.IsAny<string>()))
            .ReturnsAsync(new Address(uuid, CITY, STREET));
        var expected = new Address(uuid, CITY, STREET) ;
        var sqldbCommand = new SqlDbCommand<Address, Guid>(connectionMock.Object);

        var actual = await sqldbCommand.GetAsync(uuid);

        Assert.Equal(expected, actual);        
    }
}
