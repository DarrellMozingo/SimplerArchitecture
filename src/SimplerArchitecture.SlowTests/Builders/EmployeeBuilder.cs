using SimplerArchitecture.Domain;

namespace SimplerArchitecture.SlowTests.Builders
{
	public class EmployeeBuilder
	{
		private readonly Employee _newEmployee = new Employee { FirstName = "default-first", LastName = "default-last" };

		public Employee Build()
		{
			return _newEmployee;
		}
	}
}