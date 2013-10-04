using System;
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

		public static string MongoAuthAdminDatabaseName
		{
			get
			{
				return GetValue("MongoAuthAdminDatabaseName", "AuthAdminDatabase");
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

		public static string LocalizationFilePath
		{
			get
			{
				return GetValue("LocalizationFilePath", string.Format(@"{0}Data\LocalizationFiles", BaseDirectory));
			}
		}

		public static string PermissionsDirectory
		{
			get
			{
				return GetValue("PermissionsDirectory", string.Format(@"{0}Data\Permissions", BaseDirectory));
			}
		}

		public static string BaseDirectory
		{
			get
			{
				return AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		private static string GetValue(string key, string defaultValue = "")
		{
			string returnValue = ConfigurationManager.AppSettings[key];

			return string.IsNullOrEmpty(returnValue) ? defaultValue : returnValue;
		}
	}
}