using ServiceStack.Configuration;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SingletonTheory.Web
{
	public class AppHostHttpListener : AppHostHttpListenerBase
	{
		public AppHostHttpListener(string serviceName, Assembly assembly) : base(serviceName, assembly) { }

		public override void Configure(Funq.Container container)
		{
		}
	}
}
