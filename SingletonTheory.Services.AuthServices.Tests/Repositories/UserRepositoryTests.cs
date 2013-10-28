using NUnit.Framework;
using ServiceStack.DataAccess;
using SingletonTheory.OrmLite;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories
{
	[TestFixture]
	public class UserRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				UserRepository userRepository = new UserRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateUser()
		{
			// Arrange
			UserRepository repository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);
			UserEntity entity = UserData.GetUserForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldThrowDuplicateUserException()
		{
			// Arrange
			UserRepository repository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);
			UserEntity entity = UserData.GetUserForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			try
			{
				// Act
				UserEntity duplicate = UserData.GetUserForInsert();
				duplicate = repository.Create(entity);

				Assert.Fail("Duplicate user creation should not be allowed.");
			}
			catch (DataAccessException ex)
			{
				// Assert
				if (ex.Message == "Duplicate User detected")
					Assert.Pass();

				Assert.Fail("Some other exception happened");
			}
		}

		[Test]
		public void ShouldCreateUsers()
		{
			// Arrange
			UserRepository repository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);
			List<UserEntity> entities = UserData.GetUsersForInsert();
			repository.ClearCollection();

			// Act
			entities = repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadUserWithUserName()
		{
			// Arrange
			UserRepository repository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);
			UserEntity entity = UserData.GetUserForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			UserEntity actual = repository.Read(entity.UserName);

			// Assert
			Assert.AreEqual(entity.Id, actual.Id);
		}

		[Test]
		public void ShouldReadUserWithUserNameList()
		{
			// Arrange
			UserRepository repository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);
			List<UserEntity> entities = UserData.GetUsersForInsert();
			repository.ClearCollection();
			entities = repository.Create(entities);
			List<string> userNames = entities.Select(x => x.UserName).ToList();

			// Act
			List<UserEntity> actual = repository.Read(userNames);

			// Assert
			Assert.AreEqual(entities.Count, actual.Count);
		}

		[Test]
		public void ShouldReadUserWithIdList()
		{
			// Arrange
			UserRepository repository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);
			List<UserEntity> entities = UserData.GetUsersForInsert();
			repository.ClearCollection();
			entities = repository.Create(entities);
			List<long> userNames = entities.Select(x => x.Id).ToList();

			// Act
			List<UserEntity> actual = repository.Read(userNames);

			// Assert
			Assert.AreEqual(entities.Count, actual.Count);
		}

		[Test]
		public void ShouldUpdateUser()
		{
			// Arrange
			UserRepository repository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);
			UserEntity entity = UserData.GetUserForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Meta.Add("UpdateField", "FieldUpdated");


			// Act
			UserEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Meta["UpdateField"], actual.Meta["UpdateField"]);
		}

		#endregion Test Methods
	}
}
