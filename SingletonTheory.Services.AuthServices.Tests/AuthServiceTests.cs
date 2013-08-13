using Bridge.AuthenticationServices.TransferObjects;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace Bridge.AuthenticationServices.Tests
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
	}
}
