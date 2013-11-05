using ServiceStack.WebHost.Endpoints;
using SingletonTheory.OrmLite.Extensions;
using SingletonTheory.Web.Interfaces;
using System;
using System.Reflection;

namespace SingletonTheory.Web
{
	public class AppHostHttpListener : AppHostHttpListenerBase
	{
		private Assembly[] _assemblies;
		private string _serviceName;
		private bool _assembliesSetUp;

		public bool AssembliesSetUp
		{
			get { return _assembliesSetUp; }
		}

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
			foreach (var assembly in _assemblies)
			{
				foreach (var assemblyType in assembly.GetTypes())
				{
					//check for IServiceAppHost interface
					if (assemblyType.HasInterfaceNonGeneric(typeof(IAppHostConfiguration)))
					{
						//call ConfigureAssembly
						var inst = Activator.CreateInstance(assemblyType) as IAppHostConfiguration;
						inst.Configure(container, Plugins);
					}
				}
			}

			_assembliesSetUp = true;
		}
	}
}
