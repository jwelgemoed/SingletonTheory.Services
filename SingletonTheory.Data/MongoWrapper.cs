using MongoDB.Driver;

namespace SingletonTheory.Data
{
	public static class MongoWrapper
	{
		public static MongoDatabase GetDatabase(string connectionString, string databaseName)
		{
			MongoClient mongoClient = new MongoClient();
			MongoServer server = mongoClient.GetServer();
			MongoDatabase db = server.GetDatabase(databaseName);

			return db;
		}
	}
}
