using NUnit.Framework;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Services.AuthServices.Host;
using SingletonTheory.Services.AuthServices.Services;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonTheory.Web.Tests
{
	[TestFixture]
	public class HostTestsExamples
	{
		private const string BaseUrlTest = "http://localhost:54720";
		private const string ListeningOn = BaseUrlTest + "/";
				
		AppHostHttpListener appHostServices;

		static IRestClient[] RestClients = 
		{
			new JsonServiceClient( ListeningOn)
		};


		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			appHostServices = new AppHostHttpListener("UserService", typeof(UserService).Assembly);
			AppHost.Configure(appHostServices.Container, appHostServices.Plugins);
			AppHost.CreateMockData(appHostServices.Container);
			appHostServices.Init();
			appHostServices.Start(ListeningOn);
		}
				
		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			appHostServices.Dispose();
		}

		[Test]
		public void ShouldRunUserService()
		{
			// Arrange
			User request = new User() { Id = 1 };
			Auth userAuth = new Auth() { UserName = "admin", Password = "123" };
			AuthResponse authResponse = (RestClients[0] as JsonServiceClient).Send<AuthResponse>(userAuth);

			// Act
			foreach (var client in RestClients)
			{
				User response = client.Get(request);
				// Assert
				Assert.IsNotNull(response);
			}
		}
	}
}
