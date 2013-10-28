using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ServiceStack.DataAccess;
using ServiceStack.OrmLite;
using SingletonTheory.OrmLite.Config;
using SingletonTheory.OrmLite.Extensions;
using SingletonTheory.OrmLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoBuilders = MongoDB.Driver.Builders;

namespace SingletonTheory.OrmLite.Providers
{
	public class MongoProvider : IDatabaseProvider
	{
		#region Fields & Properties

		private MongoDatabase _databaseConnection;
		private Dictionary<string, ModelDefinition> _modelDefinitions = new Dictionary<string, ModelDefinition>();

		public bool HasTransactionSupport
		{
			get
			{
				return false;
			}
		}

		#endregion Fields & Properties

		#region Constructors

		public MongoProvider(string connectionString)
		{
			if (string.IsNullOrEmpty(connectionString))
				throw new ArgumentNullException("connectionString");

			_databaseConnection = MongoExtensions.OpenDbConnection(connectionString);
		}

		#endregion Constructors

		#region Public Methods

		public bool CollectionExists(Type modelType)
		{
			ModelDefinition modelDefinition = GetModelDefinition(modelType);

			return _databaseConnection.CollectionExists(modelDefinition.Alias ?? modelType.Name);
		}

		public void CreateCollection(Type modelType)
		{
			if (!_databaseConnection.CollectionExists(GetCollectionName(modelType)))
				_databaseConnection.CreateCollection(GetCollectionName(modelType));
		}

		public void DropAndCreate(Type modelType)
		{
			_databaseConnection.DropCollection(GetCollectionName(modelType));
			_databaseConnection.DropCollection(GetCounterCollectionName(modelType));
			_databaseConnection.CreateCollection(GetCollectionName(modelType));
		}

		private string GetCollectionName(Type modelType)
		{
			ModelDefinition modelDefinition = GetModelDefinition(modelType);

			if (modelDefinition == null)
				return modelType.Name;

			return modelDefinition.Alias ?? modelType.Name;
		}

		private ModelDefinition GetModelDefinition(Type fieldType)
		{
			ModelDefinition modelDefinition;

			if (!_modelDefinitions.TryGetValue(fieldType.Name, out modelDefinition))
			{
				modelDefinition = fieldType.GetModelDefinition();
				if (modelDefinition == null)
					return null;

				_modelDefinitions.Add(fieldType.Name, modelDefinition);
			}

			return modelDefinition;
		}

		public T SelectById<T>(long idValue) where T : IIdentifiable
		{
			try
			{
				MongoCollection<T> collection = _databaseConnection.GetCollection<T>(GetCollectionName(typeof(T)));
				IMongoQuery query = MongoBuilders.Query<T>.EQ(e => e.Id, idValue);
				T entity = collection.FindOne(query);

				return entity == null ? default(T) : entity;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public List<T> Select<T>() where T : IIdentifiable
		{
			try
			{
				MongoCollection<T> collection = _databaseConnection.GetCollection<T>(GetCollectionName(typeof(T)));
				MongoCursor<T> cursor = collection.FindAllAs<T>();

				return cursor.ToList();
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public List<T> Select<T>(Expression<Func<T, bool>> predicate) where T : IIdentifiable
		{
			try
			{
				string collectionName = GetCollectionName(typeof(T));
				MongoCollection<T> collection = _databaseConnection.GetCollection<T>(collectionName);
				IQueryable<T> queryable = collection.AsQueryable<T>();

				return queryable.Where(predicate).ToList();
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public T Insert<T>(T objectToInsert) where T : IIdentifiable, new()
		{
			try
			{
				objectToInsert.SetId(GetNextCounter<T>()); // Update the Id of the new item before it gets inserted
				MongoCollection<T> collection = _databaseConnection.GetCollection<T>(GetCollectionName(typeof(T)));
				WriteConcernResult result = collection.Insert(objectToInsert);
				if (!string.IsNullOrEmpty(result.ErrorMessage))
					throw new DataAccessException("Data Insert Error:  " + result.ErrorMessage);

				return objectToInsert;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Unable to insert record in the Mongo Database: " + ex.Message);
			}
		}

		public void Update<T>(T objectToUpdate) where T : IIdentifiable, new()
		{
			try
			{
				objectToUpdate.SetId(GetNextCounter<T>()); // Update the Id of the new item before it gets inserted
				MongoCollection<T> collection = _databaseConnection.GetCollection<T>(GetCollectionName(typeof(T)));
				IMongoQuery query = MongoBuilders.Query<T>.EQ(e => e.Id, objectToUpdate.Id);
				T duplicate = collection.FindOne(query);

				if (duplicate != null)
					throw new DataAccessException("Item not found");

				WriteConcernResult result = collection.Update(query, MongoBuilders.Update.Replace(objectToUpdate), UpdateFlags.Upsert);

				if (!string.IsNullOrEmpty(result.ErrorMessage))
					throw new DataAccessException("Data Insert Error:  " + result.ErrorMessage);
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Unable to insert record in the Mongo Database: " + ex.Message);
			}
		}

		public void Delete<T>(Expression<Func<T, bool>> where)
		{
			//_databaseConnection.Delete<T>(where);
			return;
		}

		public void Delete<T>(T objectToDelete) where T : IIdentifiable, new()
		{
			MongoCollection<T> collection = _databaseConnection.GetCollection<T>(GetCollectionName(typeof(T)));
			IMongoQuery query = MongoBuilders.Query<T>.EQ(e => e.Id, objectToDelete.Id);
			WriteConcernResult result = collection.Remove(query);

			if (!string.IsNullOrEmpty(result.ErrorMessage))
				throw new DataAccessException("Data Delete Error:  " + result.ErrorMessage);
		}

		public void DeleteAll<T>()
		{
			DropAndCreate(typeof(T));
		}

		public void Rollback()
		{
			//if (_transaction == null || _transaction.Connection == null)
			//	throw new InvalidOperationException("Transaction not started or closed.");

			//_transaction.Rollback();
		}

		private long GetNextCounter<T>()
		{
			string collectionName = GetCounterCollectionName(typeof(T));

			if (!_databaseConnection.CollectionExists(collectionName))
			{
				InsertFirstCounter(collectionName);
				return 1;
			}

			MongoCollection<Counters> collection = _databaseConnection.GetCollection<Counters>(collectionName);
			MongoBuilders.UpdateBuilder incrementId = MongoBuilders.Update.Inc("IdValue", 1);
			IMongoQuery query = MongoBuilders.Query.Null;
			FindAndModifyResult counterIncResult = collection.FindAndModify(query, MongoBuilders.SortBy.Null, incrementId, true);
			Counters updatedCounters = counterIncResult.GetModifiedDocumentAs<Counters>();

			return updatedCounters.IdValue;
		}

		private string GetCounterCollectionName(Type modelType)
		{
			return GetCollectionName(modelType) + "_Counter";
		}

		private void InsertFirstCounter(string counterCollectionName)
		{
			_databaseConnection.CreateCollection(counterCollectionName);
			Counters objectToInsert = new Counters();

			objectToInsert.IdValue = 1;
			MongoCollection<Counters> collection = _databaseConnection.GetCollection<Counters>(counterCollectionName);
			WriteConcernResult result = collection.Insert(objectToInsert);

			if (!string.IsNullOrEmpty(result.ErrorMessage))
				throw new DataAccessException("Data Insert Error:  " + result.ErrorMessage);
		}

		#endregion Public Methods

		#region IDisposable Members

		public void Dispose()
		{
			if (_databaseConnection != null)
			{
				_databaseConnection = null;
			}
		}

		#endregion IDisposable Members

		#region Internal Classes

		class Counters
		{
			public ObjectId Id { get; set; }
			public long IdValue { get; set; }
		}

		#endregion Internal Classes
	}
}