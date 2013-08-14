using System.Linq;
using System.Web.Mvc;
using NHibernate.Linq;
using SimplerArchitecture.Domain;
using SimplerArchitecture.Infrastructure;
using SimplerArchitecture.Web.Models;

namespace SimplerArchitecture.Web.Controllers
{
	public class EmployeeListController : SimplerArchitectureController
	{
		public ActionResult List()
		{
			var employees = DatabaseSession.Query<Employee>()
			                               .OrderBy(x => x.LastName)
			                               .ToList();

			var viewModels = employees.Select(x => new EmployeeListViewModel
				{
					Id = x.Id,
					FullName = x.FirstName + " " + x.LastName
				});

			return View(viewModels);
		}
	}
}