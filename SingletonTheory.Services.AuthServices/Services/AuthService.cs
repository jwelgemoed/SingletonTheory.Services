using ServiceStack.Common;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class AuthService : Service
	{
		#region Public Methods

		public UserAuth Get(CurrentUserAuthRequest request)
		{
			IAuthSession session = this.GetSession();
			IUserAuthRepository repository = GetRepository();

			return repository.GetUserAuthByUserName(session.UserName);
		}

		#endregion Public Methods

		#region Private Methods

		private IUserAuthRepository GetRepository()
		{
			IUserAuthRepository repository = base.GetResolver().TryResolve<IUserAuthRepository>();
			return repository;
		}

		#endregion Private Methods
	}
}