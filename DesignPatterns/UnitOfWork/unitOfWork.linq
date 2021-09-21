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
	RunTests();
}

/*
# Requirements:
- As a HR personel, i would like to increase the salary of the employee by using his/her email address and amount of increase 
and also if i will not give any increase amount of the salary, it sould calculate automatically according to old increases.
- if there is no increase before, as default %10 percent must be increased the current salary.

# Story
- Core system is already given you these interfaces. 
- You can only play in the one class that implements the IEmployeeBusiness interface

# Technical 
- IncreaseSalary method
  - increase the salary according to the email
  - if no increase given, than default increase will be calculated and given inside the method.
  - if increase given, given increase must be used
- CalculateNextSalaryIncrease method
  - with given all salary increases, it calculates the next salary increase with only using odd indexes in the list.

# INFO:
you are the only developer who knows only these interfaces and that's it. no database, no system. nothing.
you need to make sure that given requirements for the methods will be fulfilled!

# Approach:
TDD

*/


#region implementation
public class EmployeeBusiness : IEmployeeBusiness
{
	public virtual decimal CalculateNextSalaryIncrease(decimal[] previousSalaryIncreases)
	{
		return 0;
	}

	public void IncreaseSalary(string email, decimal? salaryIncrease = null)
	{
		var employee = GetEmployeeByEmail(email);

		if (salaryIncrease == null)
		{
			salaryIncrease = CalculateNextSalaryIncrease(null);
		}

	}

	//returns: valid employee?
	//returns: exception?
	//returns: null?
	internal Employee GetEmployeeByEmail(string email)
	{
		return new Employee();
	}
}
#endregion


#region Do not change it
public interface IEmployeeBusiness
{
	void IncreaseSalary(string email, decimal? salaryIncrease = null);
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

public interface IDbEntity
{
	int Id { get; set; }
}
public interface IRepository<Entity> where Entity : IDbEntity
{
	Entity GetById(int id);
	IEnumerable<Entity> Get(Expression<Func<Entity, bool>> filter);
	Entity Update(Entity entity);
	Entity Insert(Entity entity);
	void Insert(int id);
}

public class Employee : IDbEntity
{
	public int Id { get; set; }
	public string EMail { get; set; }
	public decimal CurrentSalary { get; set; }
}

public class SalaryLog : IDbEntity
{
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public decimal PreviousSalary { get; set; }
	public decimal CurrentSalary { get; set; }
}
#endregion


#region private::Tests

private Mock<EmployeeBusiness> createEmployeeBusinessMock(bool callBase = true)
{
	var employeeBusinessMock = new Mock<EmployeeBusiness>();
	employeeBusinessMock.CallBase = callBase;
	
	return employeeBusinessMock;
}

[Fact]
void When_IncreaseSalary_called_And_salaryIncrease_Notnull_Then_CalculateNextSalaryIncrease_should_not_called()
{
	//arrange
	var employeeBusinessMock = createEmployeeBusinessMock();
	
	//act
	employeeBusinessMock.Object.IncreaseSalary(It.IsAny<string>(), 10.0m); 
	
	//assert
	employeeBusinessMock.Verify(m => m.CalculateNextSalaryIncrease(It.IsAny<decimal[]>()), Times.Never);
}

[Fact]
void When_IncreaseSalary_called_And_salaryIncrease_Null_Then_CalculateNextSalaryIncrease_should_be_called()
{
	//arrange
	var employeeBusinessMock = createEmployeeBusinessMock();

	//act
	employeeBusinessMock.Object.IncreaseSalary(It.IsAny<string>(), null);

	//assert
	employeeBusinessMock.Verify(m => m.CalculateNextSalaryIncrease(It.IsAny<decimal[]>()), Times.Once);
}

[Fact]
void When_getEmployeeByEmail_called_with_not_empty_email_then_Employee_should_be_returned()
{
	//arrange
	var employeeBusinessMock = createEmployeeBusinessMock();

	//act
	var employee = employeeBusinessMock.Object.GetEmployeeByEmail(It.IsAny<string>());
	
	//assert
	Assert.IsType(typeof(Employee), employee);
}

[Fact]
void When_IncreaseSalary_called_Then_employee_data_must_be_get_by_email()
{
	//arrange
	true.Should().BeFalse("implement me!");
	//act

	//assert
}



#endregion