using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace SingletonTheory.OrmLite.Extensions
{
	public static class MongoExtensions
	{
		public static MongoDatabase OpenDbConnection(string connectionString)
		{
			if (string.IsNullOrEmpty(connectionString))
				throw new ArgumentException("Connection String cannot be empty or null");

			Dictionary<string, string> connectionDictionary = GetConnectionStringParts(connectionString);

			ValidateConnectionString(connectionDictionary);

			return GetDatabase(string.Format("mongodb://{0}", connectionDictionary["Server"]), connectionDictionary["Database"]);
		}

		private static void ValidateConnectionString(Dictionary<string, string> connectionDictionary)
		{
			string outValue;
			if (!connectionDictionary.TryGetValue("Server", out outValue))
			{
				throw new ArgumentException("Server needs to be specified in connection string");
			}

			if (!connectionDictionary.TryGetValue("Database", out outValue))
			{
				throw new ArgumentException("Database needs to be specified in connection string");
			}
		}

		private static Dictionary<string, string> GetConnectionStringParts(string connectionString)
		{
			string[] connectionStringItems = connectionString.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			Dictionary<string, string> connectionDictionary = new Dictionary<string, string>();
			for (int i = 0; i < connectionStringItems.Length; i++)
			{
				string[] items = connectionStringItems[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
				connectionDictionary.Add(items[0], items[1]);
			}
			return connectionDictionary;
		}

		private static MongoDatabase GetDatabase(string connectionString, string databaseName)
		{
			MongoClient mongoClient = new MongoClient();
			MongoServer server = mongoClient.GetServer();
			MongoDatabase database = server.GetDatabase(databaseName);

			return database;
		}
	}
}
