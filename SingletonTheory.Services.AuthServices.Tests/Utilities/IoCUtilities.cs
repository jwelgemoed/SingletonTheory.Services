using ServiceStack.WebHost.Endpoints;
using System;

namespace SingletonTheory.Services.AuthServices.Tests.Utilities
{
	public static class IoCUtilities
	{
		#region Fields & Properties

		private static Funq.Container _container;

		#endregion Fields & Properties

		#region Constructors

		static IoCUtilities()
		{
			_container = EndpointHost.Config.ServiceManager.Container;
		}

		#endregion Constructors

		public static T GetRepository<T>()
		{
			T repository = _container.Resolve<T>();

			if (repository == null)
				throw new InvalidOperationException(string.Format("Respository of Type:  [{0}] not found in the IoC Container.", typeof(T)));

			return repository;
		}
	}
}
