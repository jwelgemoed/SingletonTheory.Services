using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using System.Net;

namespace SingletonTheory.Services.AuthServices.Tests.Services
{
	[TestFixture]
	public class AuthenticationTests
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

		//[Test]
		//public void ShouldAuthenticateUser()
		//{
		//	// Arrange
		//	Auth request = new Auth { UserName = HTTPClientHelpers.UserName, Password = HTTPClientHelpers.Password };

		//	// Act
		//	AuthResponse response = _client.Send<AuthResponse>(request);

		//	// Assert
		//	Assert.That(response.UserName, Is.EqualTo(request.UserName));
		//}

		//[Test]
		//public void ShouldAuthenticateAdminUser()
		//{
		//	// Arrange
		//	Auth request = new Auth { UserName = HTTPClientHelpers.AdminUserName, Password = HTTPClientHelpers.Password };

		//	// Act
		//	AuthResponse response = _client.Send<AuthResponse>(request);

		//	// Assert
		//	Assert.That(response.UserName, Is.EqualTo(request.UserName));
		//}

		//[Test]
		//public void ShouldThrowUnauthorisedException()
		//{
		//	try
		//	{
		//		// Arrange
		//		string password = "wrongpassword";
		//		Auth request = new Auth { UserName = HTTPClientHelpers.UserName, Password = password };

		//		// Act
		//		AuthResponse response = _client.Send<AuthResponse>(request);

		//		Assert.Fail("Shouldn't be allowed:  Invalid Credentials");
		//	}
		//	catch (WebServiceException webEx)
		//	{
		//		// Assert
		//		Assert.That(webEx.StatusCode, Is.EqualTo((int)HttpStatusCode.Unauthorized));
		//	}
		//}

		//[Test]
		//public void ShouldLogoutUser()
		//{
		//	AuthResponse response = HTTPClientHelpers.Logout(_client);

		//	Assert.That(response.UserName, Is.EqualTo(null));
		//}

		#endregion Test Methods
	}
}
