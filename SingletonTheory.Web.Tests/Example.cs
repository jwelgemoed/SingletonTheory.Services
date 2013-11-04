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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace SingletonTheory.Web.Tests
{
	class Example
	{
		private const string BaseUrlTest = "http://localhost:10101";
		private const string ListeningOn = BaseUrlTest + "/";

		AppHostHttpListener appHostServices;

		static IRestClient[] RestClients = 
		{
			new JsonServiceClient( ListeningOn)
			//new XmlServiceClient(ServiceClientBaseUri),
		};

		public void ExampleServiceCall()
		{
			appHostServices = new AppHostHttpListener("UserService", typeof(UserService).Assembly);
			appHostServices.Init();
			appHostServices.Start(ListeningOn);

			AppHost.Configure(appHostServices.Container, appHostServices.Plugins);
			AppHost.CreateMockData(appHostServices.Container);

			Auth userAuth = new Auth() { UserName = "admin", Password = "123" };
			AuthResponse authResponse = (RestClients[0] as JsonServiceClient).Send<AuthResponse>(userAuth);
			User request = new User() { Id = 1 };

			foreach (var client in RestClients)
			{
				User response = client.Get(request);
			}

			appHostServices.Dispose();
		}
	}
}
