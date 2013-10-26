using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.Common.Utils;
using SingletonTheory.Data;
using SingletonTheory.Library.IO;
using SingletonTheory.Services.AuthServices.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class GenericRepository
	{
		#region Constants

		public const string RolesCollection = "Roles";
		public const string FunctionalPermissionsCollection = "FunctionalPermissions";
		public const string DomainPermissionsCollection = "DomainPermissions";
		public const string PermissionsCollection = "Permissions";

		#endregion Constants

		#region Fields & Properties

		private static MongoDatabase _mongoDatabase;

		#endregion Fields & Properties

		#region Public Methods

		public static List<T> GetList<T>(string dataBaseName, string collectionName)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);
			MongoCursor<T> cursor = collection.FindAllAs<T>();

			return cursor.ToList();
		}

		public static T GetItemByMongoQuery<T>(string dataBaseName, string collectionName, IMongoQuery query)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);

			var cursor = collection.FindOneAs<T>(query);

			return cursor;
		}

		public static List<T> GetItemById<T>(string dataBaseName, string collectionName, int id)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);

			MongoCursor<T> cursor = collection.Find(Query.EQ("_id", id));

			return cursor.ToList();
		}

		public static T GetItemTopById<T>(string dataBaseName, string collectionName, int id)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);

			var cursor = collection.FindOneAs<T>(Query.EQ("_id", id));

			return cursor;
		}

		public static T Add<T>(string dataBaseName, string collectionName, T obj, bool writeFile = true)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);

			if(obj != null)
				collection.Save(obj);

			if (writeFile)
			{
				List<T> listToWrite = GetList<T>(dataBaseName, collectionName);
				WriteToFile(listToWrite, typeof(T).Name + ".json");
			}

			return obj;
		}

		private static void WriteToFile(object obj, string fileName)
		{
			SerializationUtilities.WriteToFile(ConfigSettings.PermissionsDirectory + @"\" + fileName, obj);
		}

		public static bool DeleteById<T>(string dataBaseName, string collectionName, int id)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			_mongoDatabase.GetCollection<T>(collectionName).Remove(Query.EQ("_id", id));

			return true;
		}

		public static int GetMaxIdIncrement<T>(string dataBaseName, string collectionName)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);
			var collection = _mongoDatabase.GetCollection<T>(collectionName);
			int id = 0;
			MongoCursor<T> cursor = collection.FindAllAs<T>();
			foreach (var col in cursor)
			{
				id = Math.Max(id, (int)col.GetId());
			}

			return id + 1;
		}

		public static void ClearCollection(string dataBaseName)
		{
			_mongoDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, dataBaseName);

			if (_mongoDatabase.CollectionExists(RolesCollection))
				_mongoDatabase.DropCollection(RolesCollection);

			if (_mongoDatabase.CollectionExists(FunctionalPermissionsCollection))
				_mongoDatabase.DropCollection(FunctionalPermissionsCollection);

			if (_mongoDatabase.CollectionExists(DomainPermissionsCollection))
				_mongoDatabase.DropCollection(DomainPermissionsCollection);

			if (_mongoDatabase.CollectionExists(PermissionsCollection))
				_mongoDatabase.DropCollection(PermissionsCollection);
		}

		#endregion Public Methods
	}
}