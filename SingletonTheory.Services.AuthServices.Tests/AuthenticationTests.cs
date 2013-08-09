using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using System.Net;

namespace SingletonTheory.Services.AuthServices.Tests
{
	[TestFixture]
	public class AuthenticationTests
	{
		#region Test Methods

		[Test]
		public void ShouldAuthenticateUser()
		{
			// Arrange
			JsonServiceClient client = HTTPClientHelpers.GetClient(HTTPClientHelpers.RootUrl, HTTPClientHelpers.UserName, HTTPClientHelpers.Password);
			Auth request = new Auth { UserName = HTTPClientHelpers.UserName, Password = HTTPClientHelpers.Password };

			// Act
			AuthResponse response = client.Send<AuthResponse>(request);

			// Assert
			Assert.That(response.UserName, Is.EqualTo(request.UserName));
		}

		[Test]
		public void ShouldThrowUnauthorisedException()
		{
			try
			{
				// Arrange
				string userName = "user";
				string password = "wrongPassword";
				JsonServiceClient client = HTTPClientHelpers.GetClient(HTTPClientHelpers.RootUrl, userName, password);
				Auth request = new Auth { UserName = userName }; // Password = password

				// Act
				AuthResponse response = client.Send<AuthResponse>(request);

				Assert.Fail("Shouldn't be allowed:  Invalid Credentials");
			}
			catch (WebServiceException webEx)
			{
				// Assert
				Assert.That(webEx.StatusCode, Is.EqualTo((int)HttpStatusCode.Unauthorized));
			}
		}

		#endregion Test Methods
	}
}
