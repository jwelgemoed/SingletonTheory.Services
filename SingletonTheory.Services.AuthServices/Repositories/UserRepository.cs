using ServiceStack.DataAccess;
using SingletonTheory.OrmLite;
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
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public UserRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(UserEntity)))
					provider.CreateCollection(typeof(UserEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public UserEntity Read(string userName)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				UserEntity entity = provider.Select<UserEntity>(x => x.UserName == userName).First();

				return entity == null ? null : entity;
			}
		}

		public UserEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<UserEntity>(id);
			}
		}

		public List<UserEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<UserEntity>();
			}
		}

		public List<UserEntity> Read(List<string> userNames)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<UserEntity>(x => userNames.Contains(x.UserName));
			}
		}

		public List<UserEntity> Read(List<long> ids)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<UserEntity>(x => ids.Contains(x.Id));
			}
		}

		public UserEntity Create(UserEntity user)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				EncryptPassword(user);
				CheckDuplicateUser(user);

				return provider.Insert<UserEntity>(user);
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
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				UserEntity userToUpdate = Read(user.UserName);
				if (userToUpdate == null)
					throw new DataAccessException("User not found"); //  This should not happen seeing that validation should check.

				userToUpdate = UpdateProperties(user, userToUpdate);

				provider.Update<UserEntity>(userToUpdate);

				return userToUpdate;
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
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(UserEntity)))
					provider.DeleteAll<UserEntity>();
			}
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