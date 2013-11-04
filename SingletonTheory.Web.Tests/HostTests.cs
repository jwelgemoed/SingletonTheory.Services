using NUnit.Framework;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using SingletonTheory.Web.Tests.AssemblyOne;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SingletonTheory.Web.Tests
{
	[TestFixture]
	public class HostTests
	{
		#region Constants
		
		private const string BaseUrlTest = "http://localhost:10101";
		private const string ListeningOn = BaseUrlTest + "/";

		#endregion Constants

		#region Fields & Properties

		private static IRestClient _restClient = new JsonServiceClient(ListeningOn);

		#endregion Fields & Properties

		#region Test Methods

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
			using (AppHostHttpListener appHostServices = GetAppHostHttpListener())
			{
				AssemblyOne.RequestOne requestOne = new AssemblyOne.RequestOne() { Id = 1 };
				AssemblyTwo.RequestTwo requestTwo = new AssemblyTwo.RequestTwo() { Id = 1 };

				// Act
				AssemblyOne.Response responseOne = _restClient.Get<AssemblyOne.Response>(requestOne);
				AssemblyTwo.Response responseTwo = _restClient.Get<AssemblyTwo.Response>(requestTwo);

				// Assert 
				// responseOne
				Assert.IsNotNull(responseOne);
				Assert.AreEqual(1, responseOne.AssemblyNumber);

				// responseTwo
				Assert.IsNotNull(responseTwo);
				Assert.AreEqual(2, responseTwo.AssemblyNumber);
			}
		}

		[Test]
		public void ShouldConfigureAssembliesWithContainerItem()
		{
			// Arrange & Act
			using (AppHostHttpListener appHostServices = GetAppHostHttpListener())
			{
				// Assert
				Assert.IsNotNull(appHostServices.Container.Resolve<AssemblyOneContainerItem>());
			}
		}

		[Test]
		public void ShouldConfigureAssembliesWithPlugin()
		{
			// Arrange & Act
			using (AppHostHttpListener appHostServices = GetAppHostHttpListener())
			{
				// Assert
				for (int i = 0; i < appHostServices.Plugins.Count; i++)
				{
					if (appHostServices.Plugins[i] is AssemblyOnePlugin)
					{
						// Assert
						Assert.Pass();
					}
				}

				Assert.Fail("This should not happen.");
			}
		}

		#endregion Test Methods

		#region Helper Methods

		private static List<Assembly> GetAssembliesToLoad()
		{
			List<Assembly> assemblies = new List<Assembly>();

			assemblies.Add(typeof(AssemblyOne.Service).Assembly);
			assemblies.Add(typeof(AssemblyTwo.Service).Assembly);

			return assemblies;
		}

		public AppHostHttpListener GetAppHostHttpListener()
		{
			List<Assembly> assemblies = GetAssembliesToLoad();
			AppHostHttpListener appHostServices = new AppHostHttpListener("TestServices", assemblies.ToArray());

			appHostServices.Init();
			appHostServices.Start(ListeningOn);

			return appHostServices;
		}

		#endregion Helper Methods
	}
}

