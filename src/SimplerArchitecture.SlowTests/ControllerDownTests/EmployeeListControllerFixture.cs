using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SimplerArchitecture.SlowTests.Builders;
using SimplerArchitecture.SlowTests.Helpers;
using SimplerArchitecture.Web.Controllers;
using SimplerArchitecture.Web.Models;

namespace SimplerArchitecture.SlowTests.ControllerDownTests
{
	public class EmployeeListControllerFixture : ControllerDownTestBase<EmployeeListController>
	{
		[Test]
		public void Lists_employees_ordered_by_name()
		{
			var employee1 = new EmployeeBuilder().FirstName("Ford").LastName("Prefect").Build(_session);
			var employee2 = new EmployeeBuilder().FirstName("Zaphod").LastName("Beeblebrox").Build(_session);

			var actionResult = callAction(x => x.List());

			var viewModel = actionResult.Model<IEnumerable<EmployeeListViewModel>>();

			Assert.That(viewModel.Count(), Is.EqualTo(2));

			Assert.That(viewModel.First().Id, Is.EqualTo(employee2.Id));
			Assert.That(viewModel.First().FullName, Is.EqualTo("Zaphod Beeblebrox"));
			
			Assert.That(viewModel.Last().Id, Is.EqualTo(employee1.Id));
			Assert.That(viewModel.Last().FullName, Is.EqualTo("Ford Prefect"));
		}

		protected override EmployeeListController buildController()
		{
			return new EmployeeListController(); // pass in mocks of any needed dependencies
		}
	}
}