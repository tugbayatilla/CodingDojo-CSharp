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
CalculateNextSalaryIncrease 
- it get all the salary logs and returns mean of the odd salaries increases.
- final salary must be fetched again

you are the developer who knows only these interfaces and that's it. no database, no system. nothing.
you need to make sure that given requirements for the methods will be covered by unit tests.




*/


#region implementation
public class EmployeeBusiness : IEmployeeBusiness
{
	public decimal CalculateNextSalaryIncrease(decimal[] previousSalaryIncreases)
	{
		throw new NotImplementedException();
	}

	public decimal IncreaseSalary(string name, decimal? salaryIncrease = null)
	{
		throw new NotImplementedException();
	}
}
#endregion


#region Do not change it
public interface IEmployeeBusiness
{
	decimal IncreaseSalary(string name, decimal? salaryIncrease = null);
	decimal CalculateNextSalaryIncrease(decimal[] previousSalaryIncreases);
}

public interface IUnitOfWork : IDisposable
{
	IDbConnection Connection { get; }
	IDbTransaction Transaction { get; }
	void Begin();
	void Commit();
	void Rollback();
}

public interface IEmployeeRepository
{
	Employee Get(string name);
	void Update(int employeeId, decimal salary);
}

public interface ISalaryLogRepository
{
	void Insert(SalaryLog log);
	SalaryLog[] Select(int employeeId);
}

public class Employee
{
	public int Id { get; set; }
	public string Name { get; set; }
	public decimal CurrentSalary { get; set; }
}

public class SalaryLog
{
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public decimal PreviousSalary { get; set; }
	public decimal CurrentSalary { get; set; }
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


#endregion