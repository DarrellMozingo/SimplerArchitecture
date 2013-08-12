using System.Data.SqlClient;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SimplerArchitecture.Data.Mappings;

namespace SimplerArchitecture.Data
{
	public class IntegrationTestDatabaseManager
	{
		private static Configuration _configuration;
		private static ISessionFactory _sessionFactory;

		public static ISession ReloadDatabase()
		{
			if (_sessionFactory == null)
			{
				_sessionFactory = createSessionFactory();
			}

			dropAllTables();
			recreateTablesFromMappings();

			var session = _sessionFactory.OpenSession();

			insertSeedData(session);

			return session;
		}

		private static ISessionFactory createSessionFactory()
		{
			ensureFreshDatabaseIsAvailable();

			_configuration = Fluently.Configure()
			                         .Database(MsSqlConfiguration.MsSql2008
			                                                     .ConnectionString(DatabaseConstants.IntegrationTests.ConnectionString))
			                         .Mappings(m => m.FluentMappings
			                                         .AddFromAssemblyOf<EmployeeMapping>())
			                         .BuildConfiguration();

			return _configuration.BuildSessionFactory();
		}

		private static void ensureFreshDatabaseIsAvailable()
		{
			var createIntegrationTestDatabaseIfNeeded = (string.Format("IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') " +
			                                             "  CREATE DATABASE [{0}]", DatabaseConstants.IntegrationTests.Database));

			executeSql(createIntegrationTestDatabaseIfNeeded, "master");
		}

		private static void dropAllTables()
		{
			const string dropAllConstraintsAndTables =
				"DECLARE @statement NVARCHAR(max); DECLARE @lineReturn CHAR(1) = CHAR(10); " +
				"SELECT @statement = ISNULL( @statement + @lineReturn, '' ) + 'ALTER TABLE [' + object_name( parent_object_id ) + '] DROP CONSTRAINT [' + name + ']' FROM sys.foreign_keys; " +
				"SELECT @statement = ISNULL( @statement + @lineReturn, '' ) + 'DROP TABLE [' + name + ']' FROM sys.tables; " +
				"EXEC sp_executesql @statement;";

			executeSql(dropAllConstraintsAndTables, DatabaseConstants.IntegrationTests.Database);
		}

		private static void recreateTablesFromMappings()
		{
			new SchemaExport(_configuration).Execute(false, true, false, _sessionFactory.OpenSession().Connection, null);
		}

		private static void executeSql(string sql, string database)
		{
			using (var connection = createSqlConnection(database))
			{
				connection.Open();

				using (var command = new SqlCommand(sql, connection))
				{
					const int fiveMinutes = 300;

					command.CommandTimeout = fiveMinutes;
					command.ExecuteNonQuery();
				}
			}
		}

		private static SqlConnection createSqlConnection(string database)
		{
			var connectionString = DatabaseConstants.BuildConnectionString(DatabaseConstants.IntegrationTests.Server,
			                                                               database,
			                                                               DatabaseConstants.IntegrationTests.Username,
			                                                               DatabaseConstants.IntegrationTests.Password);

			return new SqlConnection(connectionString);
		}

		private static void insertSeedData(ISession session)
		{
			// TODO: Insert as needed.
		}
	}
}