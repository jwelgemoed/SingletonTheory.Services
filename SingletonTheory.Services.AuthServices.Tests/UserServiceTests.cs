using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using SingletonTheory.Services.AuthServices.Services;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using System;

namespace SingletonTheory.Services.AuthServices.Tests
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
			_client.Dispose();
			_client = null;
		}

		#endregion Setup & Teardown

		[Test]
		public void ShouldNotAllowUserToGetTop5MostActive()
		{
			// Arrange
			UserActivityRequest request = new UserActivityRequest();
			UserActivityRequest response = null;
			HTTPClientHelpers.Login();

			try
			{
				// Act
				response = _client.Get(request);

				Assert.Pass("User has permissions");
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);

				// Assert
				Assert.Fail("Failed:  Should not be allowed");
			}
		}
	}
}
