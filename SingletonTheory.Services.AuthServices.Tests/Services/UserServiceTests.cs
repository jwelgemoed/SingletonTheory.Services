using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Tests.Data;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonTheory.Services.AuthServices.Tests.Services
{
	[TestFixture]
	public class UserServiceTests
	{
		#region Fields & Properties

		private JsonServiceClient _client;

		#endregion Fields & Properties

		#region Setup & Teardown

		[SetUp]
		public void SetUp()
		{
			_client = HTTPClientHelpers.GetClient(HTTPClientHelpers.RootUrl);
		}

		[TearDownAttribute]
		public void TearDown()
		{
			HTTPClientHelpers.Logout(_client);

			_client.Dispose();
			_client = null;
		}

		#endregion Setup & Teardown

		#region Test Methods

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
		public void ShouldGetAllUsers()
		{
			// Arrange
			User request = new User();

			// Act
			List<User> response = _client.Get(request);

			// Assert
			Assert.AreEqual(response.Count, 3);
		}

		[Test]
		public void ShouldGetUserWithUserName()
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
