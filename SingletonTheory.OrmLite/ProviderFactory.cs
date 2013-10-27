using SingletonTheory.OrmLite.Config;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.OrmLite.Providers;
using System.Configuration;

namespace SingletonTheory.OrmLite
{
	public static class ProviderFactory
	{
		public static IDatabaseProvider GetProvider(string connectionName, bool useTransation = false)
		{
			string connectionProvider = ConfigSettings.GetConnectionProvider(connectionName);
			string connectionString = ConfigSettings.GetConnectionString(connectionName);
			switch (connectionProvider)
			{
				case "System.Data.SqlClient":
					return new SqlProvider(connectionString, useTransation);

				case "MySql.Data.MySqlClient":
					return new MySqlProvider(connectionString, useTransation);

				case "SingletonTheory.OrmLite.Data.MongoClient":
					return new MongoProvider(connectionString);

				default:
					throw new ConfigurationException("Connection provider not configured");
			}
		}
	}
}
