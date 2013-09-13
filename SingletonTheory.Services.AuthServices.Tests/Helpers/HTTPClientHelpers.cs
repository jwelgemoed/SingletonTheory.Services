using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Tests.Helpers
{
	public static class HTTPClientHelpers
	{
		#region Constants

		public const string UserName = "user";
		public const string AdminUserName = "admin";
		public const string Password = "123";
		public const string RootUrl = "http://localhost:54720/";

		#endregion Constants

		#region Helper Methods

		public static JsonServiceClient GetClient(string rootUrl)
		{
			return new JsonServiceClient(rootUrl);
		}

		public static JsonServiceClient GetClient(string rootUrl, string userName, string password)
		{
			JsonServiceClient jsonClient = new JsonServiceClient(RootUrl);

			jsonClient.UserName = UserName;
			jsonClient.Password = Password;

			return jsonClient;
		}

		public static AuthResponse Login()
		{
			JsonServiceClient client = HTTPClientHelpers.GetClient(HTTPClientHelpers.RootUrl, HTTPClientHelpers.UserName, HTTPClientHelpers.Password);
			Auth request = new Auth { UserName = HTTPClientHelpers.UserName, Password = HTTPClientHelpers.Password };
			AuthResponse response = client.Send<AuthResponse>(request);

			return response;
		}

		public static AuthResponse Logout(JsonServiceClient client)
		{
			Auth request = new Auth { provider = "logout" };

			return client.Send<AuthResponse>(request);
		}

		#endregion Helper Methods
	}
}
