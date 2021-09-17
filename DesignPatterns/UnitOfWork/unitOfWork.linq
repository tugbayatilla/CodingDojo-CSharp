<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>FluentAssertions</NuGetReference>
  <NuGetReference>Moq</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>FluentAssertions.Xml</Namespace>
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>Xunit</Namespace>
  <Namespace>Moq</Namespace>
  <Namespace>FluentAssertions</Namespace>
</Query>

#load "xunit"

void Main()
{
	RunTests();  // Call RunTests() or press Alt+Shift+T to initiate testing.
}

// You can define other methods, fields, classes and namespaces here

/*
Inc
Task:
- implement the IEmployeeBusiness
- all interfaces must be used

# requirements
- increase the salary of the employee and mark as increased in employee, return the final salary
- if something happens to should not write in database

- final salary must be fetched again

you are the developer who knows only these interfaces and that's it. no database, no system. nothing.
you need to make sure that given requirements for the methods will be covered by unit tests.
*/


#region implementation
public class EmployeeBusiness : IEmployeeBusiness
{
	private IEmployeeRepository _employeeRepository;
	
	public decimal IncreaseSalary(string name, decimal salary)
	{
		//TODO: where can i find the employeeId?   //int.Parse(name) );
		//var employee = _employeeRepository.GetById();  
		
		return salary;
	}
	
	internal Employee GetEmployeeEasily(string name){
		return null;
	}
}
#endregion


#region Do not change it
public interface IEmployeeBusiness
{
	decimal IncreaseSalary(string name, decimal salary);
}

public interface IUnitOfWork
{
	IDbConnection Connection { get; }
	IDbTransaction Transaction { get; }
	void Begin();
	void Commit();
	void Rollback();
}

public interface IEmployeeRepository
{
	Employee GetByName(string name);
	
	[Obsolete("use SetSalaryIncreasedFlag2 instead!")]
	void SetSalaryIncreasedFlag(int employeeId);
	
	void SetSalaryIncreasedFlag2(int employeeId);
}

public interface ISalaryRepository
{
	decimal GetSalary(int employeeId);
	void UpdateSalary(int employeeId, decimal salary);
}

public class Employee
{
	public int Id { get; set; }
	public string Name { get; set; }
	public bool SalaryIncreased { get; set; }
}

public class Salary
{
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public decimal Amount { get; set; }
}
#endregion


#region private::Tests

[Fact]
void When_IncreaseSalary_called_with_salary_Then_returns_value_greater_or_equal_5()
{
	//arrange
	var employeeBusiness = new EmployeeBusiness();
	var increasingSalary = 5;

	//act
	var actual = employeeBusiness.IncreaseSalary(It.IsAny<string>(), increasingSalary);

	//assert
	actual.Should().BeGreaterOrEqualTo(increasingSalary);
}

[Fact]
void When_IncreaseSalary_called_with_salary_Then_Employee_must_be_fetched()
{
	//arrange
	var employeeBusiness = new EmployeeBusiness();
	var increasingSalary = 5;
	var employeeRepositoryMock = new Mock<IEmployeeRepository>();
	//TODO: initialize (pass the mock) IEmployeeRepository in employeeBusiness class

	//act
	var actual = employeeBusiness.IncreaseSalary(It.IsAny<string>(), increasingSalary);

	//assert
	actual.Should().BeGreaterOrEqualTo(increasingSalary);
	employeeRepositoryMock.Verify(m => m.GetById(It.IsAny<int>()), Times.Once);
}

#endregion