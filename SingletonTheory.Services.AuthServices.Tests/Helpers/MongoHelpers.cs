using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Data;
using SingletonTheory.Services.AuthServices.Config;

namespace SingletonTheory.Services.AuthServices.Tests.Helpers
{
	public static class MongoHelpers
	{
		public const string MongoConnectionString = "mongodb://localhost:27017";
		public const string MongoUserDatabaseName = "UserDatabase";
		public const string MongoTestUsername = "STTestUser";
		public const string MongoTestUserPassword = "STTestUserPass";

		public static MongoDatabase GetLocalizationDatabase()
		{
			return MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, ConfigSettings.MongoLocalizationDatabaseName);
		}

		public static MongoDatabase GetUserDatabase()
		{
			return MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, ConfigSettings.MongoUserDatabaseName);
		}

		public static void DeleteAllTestUserEntries()
		{
			MongoDatabase userDatabase = MongoWrapper.GetDatabase(MongoConnectionString, MongoUserDatabaseName);
			var collection = userDatabase.GetCollection("UserAuth");
			var existingTestingUsers = collection.FindAs<UserAuth>(Query.EQ("UserName", MongoTestUsername));
			foreach (var existingTestingUser in existingTestingUsers)
			{
				collection.Remove(Query.EQ("_id", existingTestingUser.Id));
			}
		}
	}
}
