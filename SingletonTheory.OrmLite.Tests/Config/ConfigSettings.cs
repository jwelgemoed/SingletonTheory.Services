using System.Configuration;

namespace SingletonTheory.OrmLite.Tests.Config
{
	public static class ConfigSettings
	{
		#region Constants

		public const string SqlConnectionName = "SqlConnectionString";
		public const string MySqlConnectionName = "MySqlConnectionString";
		public const string MongoConnectionName = "MongoConnectionString";

		#endregion Constants

		#region Fields & Properties

		public static string SqlConnectionString
		{
			get
			{
				return GetConnectionString(SqlConnectionName);
			}
		}

		public static string MySqlConnectionString
		{
			get
			{
				return GetConnectionString(MySqlConnectionName);
			}
		}

		public static string MongoConnectionString
		{
			get
			{
				return GetConnectionString(MongoConnectionName);
			}
		}

		#endregion Fields & Properties

		#region Private Methods

		private static string GetValue(string key, string defaultValue = "")
		{
			string returnValue = ConfigurationManager.AppSettings[key];

			return string.IsNullOrEmpty(returnValue) ? defaultValue : returnValue;
		}

		private static string GetConnectionString(string key, string defaultValue = "")
		{
			string returnValue = ConfigurationManager.ConnectionStrings[key].ConnectionString;

			return string.IsNullOrEmpty(returnValue) ? defaultValue : returnValue;
		}

		#endregion Private Methods
	}
}
