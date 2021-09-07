<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>FluentAssertions</NuGetReference>
  <NuGetReference>Moq</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Xunit</Namespace>
  <Namespace>Moq</Namespace>
</Query>

#load "xunit"

void Main()
{
	RunTests();  // Call RunTests() or press Alt+Shift+T to initiate testing.
}

public class DatabaseFluent
{
	private IDbConnection _connection;

	public DatabaseFluent(IDbConnection connection)
	{
		_connection = connection;
	}

	public DatabaseFluent OpenConnection()
	{
		if (_connection.State == ConnectionState.Closed ||
			_connection.State == ConnectionState.Broken)
		{
			_connection.Open();
		}

		return this;
	}
}

// You can define other methods, fields, classes and namespaces here

#region private::Tests

private Mock<IDbConnection> _dbConnectionMock = new Mock<IDbConnection>();
Mock<DatabaseFluent> CreateMock() => new Mock<DatabaseFluent>(_dbConnectionMock.Object);

[Fact]
void OpenConnection_should_return_DatabaseFluent_as_this()
{
	// arrange
	var databaseFluent = CreateMock().Object;

	// act
	var actualDatabaseFluent = databaseFluent.OpenConnection();

	// assert
	Assert.Same(databaseFluent, actualDatabaseFluent);
}

[Fact]
void OpenConnection_should_call_connection_open_method_once()
{
	// arrange
	var databaseFluentMock = CreateMock();
	databaseFluentMock.CallBase = true;

	// act
	databaseFluentMock.Object.OpenConnection();

	// assert
	_dbConnectionMock.Verify(fm => fm.Open(), Times.Once);
}

[Fact]
void When_OpenConnection_is_called_twice_Then_Method_Open_should_be_called_once()//OpenConnection_should_be_opened_once_if_the_connection_is_closed_or_broken()
{
	// arrange
	_dbConnectionMock.Setup(m => m.Open());
	_dbConnectionMock.SetupGet(p => p.State).Returns(ConnectionState.Open);

	var databaseFluent = CreateMock().Object;

	// act
	var actualDatabaseFluent = databaseFluent.OpenConnection();

	// assert
	_dbConnectionMock.Verify(fm => fm.Open(), Times.Never);
}

[Fact]
void When_OpenConnection_called_multiple_times_then_Open_method_should_be_called_once()
{
	// arrange
	_dbConnectionMock.Setup(m => m.Open());

	var databaseFluent = CreateMock().Object;

	// act
	databaseFluent.OpenConnection();
	_dbConnectionMock.SetupGet(p => p.State).Returns(ConnectionState.Open);

	databaseFluent.OpenConnection();
	databaseFluent.OpenConnection();

	// assert
	_dbConnectionMock.Verify(fm => fm.Open(), Times.Once);
}

[Theory]
[InlineData(ConnectionState.Open, 1)]
[InlineData(ConnectionState.Broken, 1)]
void When_OpenConnection_called_multiple_times_then_Open_method_should_be_called_once(ConnectionState state, int times)
{
	// arrange
	_dbConnectionMock.Setup(m => m.Open());

	var databaseFluent = CreateMock().Object;

	// act
	databaseFluent.OpenConnection();
	_dbConnectionMock.SetupGet(p => p.State).Returns(state); //todo: continue this later

	databaseFluent.OpenConnection();
	databaseFluent.OpenConnection();

	// assert
	_dbConnectionMock.Verify(fm => fm.Open(), Times.Exactly(times));
}




#endregion