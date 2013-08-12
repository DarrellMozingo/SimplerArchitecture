using NUnit.Framework;
using SimplerArchitecture.Web.Controllers;

namespace SimplerArchitecture.SlowTests.ControllerDownTests
{
	public class EmployeeListControllerFixture : ControllerDownTestBase<EmployeeListController>
	{
		[Test]
		public void Lists_employees()
		{
			var actionResult = callAction(x => x.List());
		}

		protected override EmployeeListController buildController()
		{
			return new EmployeeListController();
		}
	}
}