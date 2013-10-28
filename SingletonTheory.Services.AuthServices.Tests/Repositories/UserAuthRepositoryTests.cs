using NUnit.Framework;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Tests.Data;
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories
{
	[TestFixture]
	public class UserAuthRepositoryTests
	{
		#region Setup & Teardown

		[SetUp]
		public void SetUp()
		{
			UserRepository repository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);

			repository.ClearCollection();
		}

		[TearDownAttribute]
		public void TearDown()
		{
			UserRepository repository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);

			repository.ClearCollection();
		}

		#endregion Setup & Teardown

		#region Test Methods

		[Test]
		public void ShouldReadUserAuthWithUserName()
		{
			// Arrange
			UserAuthRepository userAuthRepository = new UserAuthRepository(MongoHelpers.GetUserDatabase(), true);
			UserRepository userRepository = new UserRepository(ConfigSettings.UserDatabaseConnectionName);
			UserEntity entity = UserData.GetUserForInsert();
			entity = userRepository.Create(entity);

			// Act
			UserAuth actual = userAuthRepository.GetUserAuthByUserName(entity.UserName, userRepository);

			// Assert
			Assert.AreEqual(entity.Id, actual.Id);
		}

		#endregion Test Methods
	}
}
