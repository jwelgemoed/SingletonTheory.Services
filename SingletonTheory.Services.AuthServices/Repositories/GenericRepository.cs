using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.Common.Utils;
using SingletonTheory.Data;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class GenericRepository
	{
		private static MongoDatabase _mongoDatabase;

		public static List<T> GetList<T>(string dataBaseName, string collectionName)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);
			MongoCursor<T> cursor = collection.FindAllAs<T>();

			return cursor.ToList();
		}

		public static List<T> GetItemByMongoQuery<T>(string dataBaseName, string collectionName, IMongoQuery query)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);
			
			MongoCursor<T> cursor = collection.Find(query);

			return cursor.ToList();
		}

		public static List<T> GetItemById<T>(string dataBaseName, string collectionName, int id)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);

			MongoCursor<T> cursor = collection.Find(Query.EQ("Id", id));

			return cursor.ToList();
		}

		public static T Add<T>(string dataBaseName, string collectionName, T obj)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);

			collection.Save(obj);
			return obj;
		}

		public static bool DeleteById<T>(string dataBaseName, string collectionName, string id)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);

			collection.Remove(Query.EQ("Id", id));
			return true;
		}

		public static int GetMaxId<T>(string dataBaseName, string collectionName)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);
			int id = 0;
			MongoCursor<T> cursor = collection.FindAllAs<T>();
			foreach (var col in cursor)
			{
				id = Math.Max(id, (int)col.GetId());
			}

			return id;
		}

	}
}