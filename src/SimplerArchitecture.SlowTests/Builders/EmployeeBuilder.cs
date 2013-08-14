using NHibernate;
using SimplerArchitecture.Domain;

namespace SimplerArchitecture.SlowTests.Builders
{
	public class EmployeeBuilder
	{
		private readonly Employee _newEmployee = new Employee { FirstName = "default-first", LastName = "default-last" };

		public Employee Build(ISession session)
		{
			session.Save(_newEmployee);
			return _newEmployee;
		}

		public EmployeeBuilder FirstName(string firstName)
		{
			_newEmployee.FirstName = firstName;
			return this;
		}

		public EmployeeBuilder LastName(string lastName)
		{
			_newEmployee.LastName = lastName;
			return this;
		}
	}
}