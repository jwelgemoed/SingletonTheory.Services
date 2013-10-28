using ServiceStack.DataAccess;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.Services.AuthServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using SSAuthInterfaces = ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class UserRepository
	{
		#region Constants

		private const string CollectionName = "UserCollection";

		#endregion Constants

		#region Fields & Properties

		private IDatabaseProvider _databaseProvider;

		#endregion Fields & Properties

		#region Constructors

		public UserRepository(IDatabaseProvider databaseProvider)
		{
			if (databaseProvider == null)
				throw new ArgumentNullException("databaseProvider");

			_databaseProvider = databaseProvider;

			if (!_databaseProvider.CollectionExists(typeof(UserEntity)))
				_databaseProvider.CreateCollection(typeof(UserEntity));
		}

		#endregion Constructors

		#region Public Methods

		public UserEntity Read(string userName)
		{
			UserEntity entity = _databaseProvider.Select<UserEntity>(x => x.UserName == userName).First();

			return entity == null ? null : entity;
		}

		public UserEntity Read(long id)
		{
			return _databaseProvider.SelectById<UserEntity>(id);
		}

		public List<UserEntity> Read()
		{
			return _databaseProvider.Select<UserEntity>();
		}

		public List<UserEntity> Read(List<string> userNames)
		{
			return _databaseProvider.Select<UserEntity>(x => userNames.Contains(x.UserName));
		}

		public List<UserEntity> Read(List<long> ids)
		{
			return _databaseProvider.Select<UserEntity>(x => ids.Contains(x.Id));
		}

		public UserEntity Create(UserEntity user)
		{
			EncryptPassword(user);
			CheckDuplicateUser(user);

			return _databaseProvider.Insert<UserEntity>(user);
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
			UserEntity userToUpdate = Read(user.UserName);
			if (userToUpdate == null)
				throw new DataAccessException("User not found"); //  This should not happen seeing that validation should check.

			userToUpdate = UpdateProperties(user, userToUpdate);

			_databaseProvider.Update<UserEntity>(userToUpdate);

			return userToUpdate;
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
			if (_databaseProvider.CollectionExists(typeof(UserEntity)))
				_databaseProvider.DeleteAll<UserEntity>();
		}

		#endregion Public Methods

		#region Private Methods

		private void CheckDuplicateUser(UserEntity user)
		{
			UserEntity duplicate = null;
			try
			{
				duplicate = Read(user.UserName);
			}
			catch { }

			if (duplicate != null)
				throw new DataAccessException("Duplicate User detected"); //  This should not happen seeing that validation should check.
		}

		private UserEntity UpdateProperties(UserEntity user, UserEntity userToUpdate)
		{
			userToUpdate.Active = user.Active;
			userToUpdate.Language = user.Language;
			userToUpdate.Meta = user.Meta;
			userToUpdate.ModifiedDate = DateTime.UtcNow;
			userToUpdate.DomainPermissions = user.DomainPermissions;
			userToUpdate.Permissions = user.Permissions;
			userToUpdate.Roles = user.Roles;

			return userToUpdate;
		}

		private static void EncryptPassword(UserEntity user)
		{
			string hash;
			string salt;
			new SSAuthInterfaces.SaltedHash().GetHashAndSaltString(user.PasswordHash, out hash, out salt);

			user.PasswordHash = hash;
			user.Salt = salt;
		}

		#endregion Private Methods
	}
}