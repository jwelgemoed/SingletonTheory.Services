﻿using System.Configuration;

namespace SingletonTheory.OrmLite.Config
{
	public static class ConfigSettings
	{
		#region Public Methods

		public static string GetConnectionString(string key, string defaultValue = "")
		{
			string returnValue = ConfigurationManager.ConnectionStrings[key].ConnectionString;

			return string.IsNullOrEmpty(returnValue) ? defaultValue : returnValue;
		}

		public static string GetConnectionProvider(string key, string defaultValue = "")
		{
			string returnValue = ConfigurationManager.ConnectionStrings[key].ProviderName;

			return string.IsNullOrEmpty(returnValue) ? defaultValue : returnValue;
		}

		#endregion Public Methods
	}
}
