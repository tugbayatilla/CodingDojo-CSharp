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
	public decimal CalculateNextSalaryIncrease(decimal[] previousSalaryIncreases)
	{
		throw new NotImplementedException();
	}

	public void IncreaseSalary(string email, decimal? salaryIncrease = null)
	{
		throw new NotImplementedException();
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

[Fact]
void Test()=>true.Should().BeTrue();

#endregion