﻿using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Host;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices
{
	public class AuthService : Service
	{
		#region Public Methods

		public UserRoleResponse Get(UserRoleRequest request)
		{
			IAuthSession session = this.GetSession();
			UserRoleResponse response = new UserRoleResponse();

			response.UserName = session.UserName;
			response.Roles = session.Roles;

			return response;
		}

		public UserAuth Get(UserAuthRequest request)
		{
			MongoDBAuthRepository repository = AppHost.UserRepository;

			UserAuth userAuth = repository.GetUserAuth(this.GetSession().UserAuthId);

			return userAuth;
		}

		#endregion Public Methods
	}
}