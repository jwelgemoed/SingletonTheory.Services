using Funq;
using ServiceStack.WebHost.Endpoints;
using System.Collections.Generic;

namespace SingletonTheory.Web.Interfaces
{
	public interface IAppHostConfiguration
	{
		void Configure(Container container, List<IPlugin> plugins);
	}
}
