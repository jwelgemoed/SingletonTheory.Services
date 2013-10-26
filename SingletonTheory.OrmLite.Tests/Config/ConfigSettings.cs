using System.Configuration;

namespace SingletonTheory.OrmLite.Tests.Config
{
	public static class ConfigSettings
	{
		#region Fields & Properties

		public static string SqlConnectionString
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
			}
		}

		public static string MySqlConnectionString
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
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
