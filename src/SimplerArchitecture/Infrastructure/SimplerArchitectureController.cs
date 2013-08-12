using System.Web.Mvc;
using NHibernate;
using SimplerArchitecture.Data;

namespace SimplerArchitecture.Infrastructure
{
	public class SimplerArchitectureController : Controller
	{
		public ISession DatabaseSession;

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			DatabaseSession = (DatabaseSession ?? DatabaseSessionManager.GetSession());
			DatabaseSession.BeginTransaction();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			DatabaseSessionManager.EndTransaction(DatabaseSession, filterContext.Exception);
		}

		public void ExecuteOnActionExecutingForTesting()
		{
			OnActionExecuting(null);
		}

		public void ExecuteOnActionExecutedForTesting()
		{
			OnActionExecuted(new ActionExecutedContext());
		}
	}
}