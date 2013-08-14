using System.Web.Mvc;
using SimplerArchitecture.Domain;
using SimplerArchitecture.Infrastructure;
using SimplerArchitecture.Web.Models;

namespace SimplerArchitecture.Web.Controllers
{
	public class EmployeeAddController : SimplerArchitectureController
	{
		[HttpPost]
		public void Add(NewEmployeeViewModel viewModel)
		{
			DatabaseSession.Save(new Employee
				{
					FirstName = viewModel.FirstName,
					LastName = viewModel.LastName
				});
		}
	}
}