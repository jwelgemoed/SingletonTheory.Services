using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class AuthService : Service
	{
		#region Public Methods

		public UserAuth Get(CurrentUserAuthRequest request)
		{
			IAuthSession session = this.GetSession();
			if (BlackListRepository.Blacklist.Contains(session.Id))
				return null;

			UserAuthRepository repository = GetRepository() as UserAuthRepository;

			var userAuth = repository.GetUserAuthByUserNameWithFunctionalPermissions(session.UserName);

			//if(userAuth.Meta.Meta)

			return userAuth;
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