using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funq;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;

namespace SingletonTheory.Services.AuthServices.Utilities
{
	public static class SessionUtility
	{
		private static UserRepository _userRepository;

		public static UserEntity GetSessionUserEntity(IAuthSession session)
		{
			if (session == null)
				return null;

			if (_userRepository == null)
			{
				// TODO:  Inject UserRepository from Top Level
				Container container = EndpointHost.Config.ServiceManager.Container;
				_userRepository = container.Resolve<UserRepository>();
			}
			return _userRepository.Read(session.UserName);
		}

	}
}