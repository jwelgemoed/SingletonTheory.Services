using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Tests.Data;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories
{
	[TestFixture]
	public class UserRepositoryTests
	{
		#region Setup & Teardown

		[SetUp]
		public void SetUp()
		{
			UserRepository repository = new UserRepository(MongoHelpers.GetUserDatabase());

			repository.ClearCollection();
		}

		[TearDownAttribute]
		public void TearDown()
		{
			UserRepository repository = new UserRepository(MongoHelpers.GetUserDatabase());

			repository.ClearCollection();
		}

		#endregion Setup & Teardown

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
			catch (System.Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateUser()
		{
			// Arrange
			UserRepository repository = new UserRepository(MongoHelpers.GetUserDatabase());
			UserEntity entity = UserData.GetUserForInsert();

			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateUsers()
		{
			// Arrange
			UserRepository repository = new UserRepository(MongoHelpers.GetUserDatabase());
			List<UserEntity> entities = UserData.GetUsersForInsert();

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
			UserRepository repository = new UserRepository(MongoHelpers.GetUserDatabase());
			UserEntity entity = UserData.GetUserForInsert();
			entity = repository.Create(entity);

			// Act
			UserEntity actual = repository.Read(entity.UserName);

			// Assert
			Assert.AreEqual(entity.Id, actual.Id);
		}

		[Test]
		public void ShouldUpdateUser()
		{
			// Arrange
			UserRepository repository = new UserRepository(MongoHelpers.GetUserDatabase());
			UserEntity entity = UserData.GetUserForInsert();
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
