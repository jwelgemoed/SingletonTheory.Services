using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Tests
{
	[TestFixture]
	public class AuthServiceTests
	{
		[Test]
		public void ShouldGetUserRoles()
		{
			// Arrange
			JsonServiceClient client = HTTPClientHelpers.GetClient(HTTPClientHelpers.RootUrl, HTTPClientHelpers.UserName, HTTPClientHelpers.Password);
			AuthResponse authResponse = HTTPClientHelpers.Login();
			UserRoleRequest request = new UserRoleRequest { UserName = authResponse.UserName, SessionId = authResponse.SessionId };
			AuthService service = new AuthService();

			// Act
			UserRoleResponse response = client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Roles.Count, 0);
		}

		[Test]
		public void ShouldGetUsers()
		{
			// Arrange
			JsonServiceClient client = HTTPClientHelpers.GetClient(HTTPClientHelpers.RootUrl, HTTPClientHelpers.AdminUserName, HTTPClientHelpers.Password);
			AuthResponse authResponse = HTTPClientHelpers.Login();
			UserAuthRequest request = new UserAuthRequest();
			AuthService service = new AuthService();

			// Act
			UserAuth response = client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Roles.Count, 0);
		}
	}
}
