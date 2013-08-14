using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;
using SimplerArchitecture.Domain;
using SimplerArchitecture.Web.Controllers;
using SimplerArchitecture.Web.Models;

namespace SimplerArchitecture.SlowTests.ControllerDownTests
{
	public class EmployeeAddControllerFixture : ControllerDownTestBase<EmployeeAddController>
	{
		[Test]
		public void Creates_new_employee()
		{
			var viewModel = new NewEmployeeViewModel
				{
					FirstName = "new",
					LastName = "employee"
				};

			callAction(x => x.Add(viewModel));

			var newEmployee = _session.Query<Employee>().FirstOrDefault();

			Assert.That(newEmployee, Is.Not.Null);

			Assert.That(newEmployee.FirstName, Is.EqualTo("new"));
			Assert.That(newEmployee.LastName, Is.EqualTo("employee"));
		}

		protected override EmployeeAddController buildController()
		{
			return new EmployeeAddController(); // pass in mocks of any needed dependencies
		}
	}
}