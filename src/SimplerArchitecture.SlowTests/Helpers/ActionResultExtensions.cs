using System.Web.Mvc;
using NUnit.Framework;

namespace SimplerArchitecture.SlowTests.Helpers
{
	public static class ActionResultExtensions
	{
		public static MODEL Model<MODEL>(this ActionResult actionResult) where MODEL : class
		{
			Assert.That(actionResult, Is.Not.Null, "ActionResult is null when we weren't expecting it to be.");

			var viewResult = actionResult as ViewResult;
			Assert.That(viewResult, Is.Not.Null, "Didn't get a view result like we expected.");

			var model = viewResult.Model as MODEL;
			Assert.That(model, Is.Not.Null, "View model returned was null.");
			
			return model;
		}
	}
}