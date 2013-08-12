using System;
using System.Web.Mvc;
using NHibernate;
using NUnit.Framework;
using SimplerArchitecture.Data;
using SimplerArchitecture.Infrastructure;

namespace SimplerArchitecture.SlowTests.ControllerDownTests
{
	public abstract class ControllerDownTestBase<CONTROLLER> 
		where CONTROLLER : SimplerArchitectureController
	{
		private CONTROLLER _controller;

		protected ISession _session;

		protected abstract CONTROLLER buildController();

		[SetUp]
		public void Before_each_test()
		{
			_controller = buildController();
			_controller.ControllerContext = new ControllerContext();

			_session = IntegrationTestDatabaseManager.ReloadDatabase();
			_controller.DatabaseSession = _session;
		}

		[TearDown]
		public virtual void After_each_test()
		{
			_session.Dispose();
		}
		 
		protected virtual ACTION_RESULT callAction<ACTION_RESULT>(Func<CONTROLLER, ACTION_RESULT> actionToCall)
		{
			_session.Clear(); // Make sure we're not using any in-memory second-level cached entities before or after the action call.
			_controller.ExecuteOnActionExecutingForTesting();

			var actionResult = actionToCall(_controller);

			_controller.ExecuteOnActionExecutedForTesting();
			_session.Clear();

			return actionResult;
		}

		protected virtual void callAction(Action<CONTROLLER> actionToCall)
		{
			_session.Clear(); // Make sure we're not using any in-memory second-level cached entities before or after the action call.
			_controller.ExecuteOnActionExecutingForTesting();

			actionToCall(_controller);

			_controller.ExecuteOnActionExecutedForTesting();
			_session.Clear();
		}
	}
}