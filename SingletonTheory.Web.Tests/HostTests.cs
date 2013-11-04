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
	[TestFixture]
	public class HostTests
	{
		private const string BaseUrlTest = "http://localhost:10101";
		private const string ListeningOn = BaseUrlTest + "/";

		AppHostHttpListener appHostServices;

		/*		static IRestClient[] RestClients = 
				{
					new JsonServiceClient( ListeningOn)
					//new XmlServiceClient(ServiceClientBaseUri),
				};
		*/
		static IRestClient RestClient = new JsonServiceClient(ListeningOn);

		private static List<Assembly> GetAssembliesToLoad()
		{
			List<Assembly> assemblies = new List<Assembly>();

			assemblies.Add(typeof(AssemblyOne.Service).Assembly);
			assemblies.Add(typeof(AssemblyTwo.Service).Assembly);

			return assemblies;
		}


		//[TestFixtureSetUp]
		public void TestFixtureSetUpForMultipleAssemblies()
		{
			List<Assembly> assemblies = GetAssembliesToLoad();
			appHostServices = new AppHostHttpListener("TestServices", assemblies.ToArray());
			appHostServices.Init();
			appHostServices.Start(ListeningOn);
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			appHostServices.Dispose();
		}

		[Test]
		public void ShouldLoadAssemblies()
		{
			// Arrange
			List<Assembly> assemblies = GetAssembliesToLoad();

			// Act
			AppHostHttpListener appHostServices = new AppHostHttpListener("test", assemblies.ToArray());

			// Assert
			Assert.AreEqual(2, appHostServices.Assemblies.Count());

		}

		[Test]
		public void ShouldLoadServicesFromAssemblies()
		{
			// Arrange
			TestFixtureSetUpForMultipleAssemblies();
			AssemblyOne.RequestOne requestOne = new AssemblyOne.RequestOne() { Id = 1 };
			AssemblyTwo.RequestTwo requestTwo = new AssemblyTwo.RequestTwo() { Id = 1 };

			// Act
			AssemblyOne.Response responseOne = new AssemblyOne.Response();
			AssemblyTwo.Response responseTwo = new AssemblyTwo.Response();

			responseOne = RestClient.Get<AssemblyOne.Response>(requestOne);
			responseTwo = RestClient.Get<AssemblyTwo.Response>(requestTwo);

			// Assert 
			//	responseOne
			Assert.IsNotNull(responseOne);
			Assert.AreEqual(1, responseOne.AssemblyNumber);
			//	responseTwo
			Assert.IsNotNull(responseTwo);
			Assert.AreEqual(2, responseTwo.AssemblyNumber);

		}

		[Test]
		public void ShouldConfigureAssemblies()
		{
			// Arrange

			// Act

			// Assert

		}

	}
}

