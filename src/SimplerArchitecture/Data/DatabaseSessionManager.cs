using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using SimplerArchitecture.Data.Mappings;

namespace SimplerArchitecture.Data
{
	public static class DatabaseSessionManager
	{
		private static ISessionFactory ClientFactory { get; set; }

		private static ISessionFactory buildFactory()
		{
			return Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2008
						.ConnectionString(DatabaseConstants.Production.ConnectionString))
				.Mappings(m => m.FluentMappings
								.AddFromAssemblyOf<EmployeeMapping>())
				.CurrentSessionContext<WebSessionContext>()
				.BuildSessionFactory();
		}

		public static ISession GetSession()
		{
			if (ClientFactory == null)
			{
				ClientFactory = buildFactory();
			}

			if (CurrentSessionContext.HasBind(ClientFactory))
				return ClientFactory.GetCurrentSession();

			var session = ClientFactory.OpenSession();

			CurrentSessionContext.Bind(session);

			return session;
		}

		public static void EndTransaction(ISession session, Exception exception)
		{
			if (exception == null)
			{
				tryCommitTransaction(session);
			}
			else
			{
				if (session.Transaction.IsActive)
				{
					session.Transaction.Rollback();
				}
			}
		}

		private static void tryCommitTransaction(ISession session)
		{
			try
			{
				session.Transaction.Commit();
			}
			catch (Exception)
			{
				if (session.Transaction.IsActive)
				{
					session.Transaction.Rollback();
				}

				throw;
			}
		}
	}
}