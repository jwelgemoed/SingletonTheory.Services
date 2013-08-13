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

		private static string GetValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}