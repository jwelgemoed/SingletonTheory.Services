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
		private Assembly[] _assemblies;
		private string _serviceName;

		public Assembly[] Assemblies
		{
			get { return _assemblies; }
			set { _assemblies = value; }
		}

		public AppHostHttpListener(string serviceName, Assembly assembly) 
			: base(serviceName, assembly) { }

		public AppHostHttpListener(string serviceName, Assembly[] assemblies)
			: base(serviceName, assemblies)
		{
			_serviceName = serviceName;
			_assemblies = assemblies;
		}

		public override void Configure(Funq.Container container)
		{
		}

		public void Configure()
		{
			throw new NotImplementedException();
		}
	}
}
