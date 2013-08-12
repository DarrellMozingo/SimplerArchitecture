namespace SimplerArchitecture.Data
{
	public static class DatabaseConstants
	{
		public static class Production
		{
			public const string Server = @"localhost\SQLEXPRESS";
			public const string Database = "db";
			public const string Username = "user";
			public const string Password = "pass";

			public static readonly string ConnectionString = BuildConnectionString(Server, Database, Username, Password);
		}

		public static class IntegrationTests
		{
			public const string Server = @"localhost\SQLEXPRESS";
			public const string Database = "SimplerArchitectureIntegrationTests";
			public const string Username = "simplerarch_integration_tests";
			public const string Password = "simplerarch_integration_tests";

			public static readonly string ConnectionString = BuildConnectionString(Server, Database, Username, Password);
		}

		public static string BuildConnectionString(string server, string database, string username, string password)
		{
			return string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", server, database, username, password);
		}
	}
}