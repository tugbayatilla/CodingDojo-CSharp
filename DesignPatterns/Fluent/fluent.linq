<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>FluentAssertions</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

void Main()
{
	//RunTests();  // Call RunTests() or press Alt+Shift+T to initiate testing.

	Console.WriteLine("hello");
}

// You can define other methods, fields, classes and namespaces here

#region private::Tests

[Fact] void Test_Xunit() => Assert.True (1 + 1 == 2);

#endregion