using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.DataAccess;
using SingletonTheory.Services.AuthServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDBBuilders = MongoDB.Driver.Builders;
using SSAuthInterfaces = ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class UserRepository
	{
		#region Constants

		private const string CollectionName = "UserCollection";

		#endregion Constants

		#region Fields & Properties

		private MongoDatabase _mongoDatabase;

		#endregion Fields & Properties

		#region Constructors

		public UserRepository(MongoDatabase mongoDatabase)
		{
			_mongoDatabase = mongoDatabase;
		}

		#endregion Constructors

		#region Public Methods

		public UserEntity Read(string userName)
		{
			try
			{
				MongoCollection<UserEntity> users = _mongoDatabase.GetCollection<UserEntity>(CollectionName);
				IMongoQuery query = Query<UserEntity>.EQ(e => e.UserName, userName);
				UserEntity entity = users.FindOne(query);

				return entity == null ? null : entity;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public List<UserEntity> Read()
		{
			try
			{
				var users = _mongoDatabase.GetCollection<UserEntity>(CollectionName);
				MongoCursor<UserEntity> cursor = users.FindAllAs<UserEntity>();
				return cursor.ToList();
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public UserEntity Create(UserEntity user)
		{
			try
			{
				EncryptPassword(user);

				MongoCollection<UserEntity> users = _mongoDatabase.GetCollection<UserEntity>(CollectionName);
				IMongoQuery query = Query<UserEntity>.EQ(e => e.UserName, user.UserName);
				UserEntity duplicate = users.FindOne(query);

				if (duplicate != null)
					throw new DataAccessException("Duplicate User detected"); //  This should not happen seeing that validation should check.

				//if (user.Id == default(int))
				//	user.Id = IncrementUserCounter();

				WriteConcernResult result = users.Insert(user);

				if (!string.IsNullOrEmpty(result.ErrorMessage))
					throw new DataAccessException("Data Insert Error:  " + result.ErrorMessage);

				return user;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Unable to insert record in the Mongo Database: " + ex.Message);
			}
		}

		public List<UserEntity> Create(List<UserEntity> users)
		{
			for (int i = 0; i < users.Count; i++)
			{
				users[i] = Create(users[i]);
			}

			return users;
		}

		public UserEntity Update(UserEntity user)
		{
			try
			{
				MongoCollection<UserEntity> users = _mongoDatabase.GetCollection<UserEntity>(CollectionName);
				IMongoQuery query = Query<UserEntity>.EQ(e => e.UserName, user.UserName);
				UserEntity userToUpdate = users.FindOne(query);

				if (userToUpdate == null)
					throw new DataAccessException("User not found"); //  This should not happen seeing that validation should check.

				WriteConcernResult result = users.Update(query, MongoDBBuilders.Update.Replace(user), UpdateFlags.Upsert);

				if (!string.IsNullOrEmpty(result.ErrorMessage))
					throw new DataAccessException("Data Update Error:  " + result.ErrorMessage);

				return user;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Unable to update record in the Mongo Database: " + ex.Message);
			}
		}

		public List<UserEntity> Update(List<UserEntity> users)
		{
			for (int i = 0; i < users.Count; i++)
			{
				users[i] = Update(users[i]);
			}

			return users;
		}

		public void ClearCollection()
		{
			if (_mongoDatabase.CollectionExists(CollectionName))
				_mongoDatabase.DropCollection(CollectionName);
		}

		#endregion Public Methods

		#region Private Methods

		private static void EncryptPassword(UserEntity user)
		{
			string hash;
			string salt;
			new SSAuthInterfaces.SaltedHash().GetHashAndSaltString(user.PasswordHash, out hash, out salt);

			user.PasswordHash = hash;
			user.Salt = salt;
		}

		private int IncrementUserCounter()
		{
			return IncrementCounter("UserCounter").UserAuthCounter;
		}

		private Counters IncrementCounter(string counterName)
		{
			var CountersCollection = _mongoDatabase.GetCollection<Counters>(typeof(Counters).Name);
			var incId = MongoDBBuilders.Update.Inc(counterName, 1);
			var query = Query.Null;
			FindAndModifyResult counterIncResult = CountersCollection.FindAndModify(query, SortBy.Null, incId, true);
			Counters updatedCounters = counterIncResult.GetModifiedDocumentAs<Counters>();

			return updatedCounters;
		}

		#endregion Private Methods

		#region Internal Classes

		class Counters
		{
			public int Id { get; set; }
			public int UserAuthCounter { get; set; }
			public int UserOAuthProviderCounter { get; set; }
		}

		#endregion Internal Classes
	}
}