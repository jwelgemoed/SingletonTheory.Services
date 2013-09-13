using System.Configuration;

namespace SingletonTheory.Services.AuthServices.Config
{
	public static class ConfigSettings
	{
		public static string MongoConnectionString
		{
			get
			{
				return GetValue("MongoConnectionString");
			}
		}

		public static string MongoUserDatabaseName
		{
			get
			{
				return GetValue("MongoUserDatabaseName");
			}
		}

		public static string MongoUserAuthDatabaseName
		{
			get
			{
				return GetValue("MongoUserAuthDatabaseName");
			}
		}

		public static string MongoLocalizationDatabaseName
		{
			get
			{
				return GetValue("MongoLocalizationDatabaseName");
			}
		}

		public static string ServiceRootUrl
		{
			get
			{
				return GetValue("ServiceRootUrl");
			}
		}

		public static string ServiceUserName
		{
			get
			{
				return GetValue("ServiceUserName");
			}
		}

		public static string ServicePassword
		{
			get
			{
				return GetValue("ServicePassword");
			}
		}

		private static string GetValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}