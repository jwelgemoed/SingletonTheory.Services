using ServiceStack.ServiceClient.Web;

namespace SingletonTheory.Services.AuthServices.Tests.Helpers
{
	public static class HTTPClientHelpers
	{
		#region Constants

		public const string UserName = "user";
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

		#endregion Helper Methods
	}
}
