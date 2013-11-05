using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Web.Interfaces;
using System.Collections.Generic;

namespace SingletonTheory.Web.Tests.AssemblyOne
{
	public class AppHost : IAppHostConfiguration
	{
		public void Configure(Funq.Container container, List<IPlugin> plugins)
		{
			container.Register<AssemblyOneContainerItem>(new AssemblyOneContainerItem());
			plugins.Add(new AssemblyOnePlugin());
		}
	}
}
