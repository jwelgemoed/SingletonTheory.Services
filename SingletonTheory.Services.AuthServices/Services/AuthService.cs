using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Host;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class AuthService : Service
	{
		#region Public Methods

		public UserAuth Get(UserRoleRequest request)
		{
			IAuthSession session = this.GetSession();
			UserService userService = new UserService();
			List<UserAuth> response = userService.Get(new UserRequest() { UserName = session.UserName });
			if (response.Count != 0)
				return response[0];

			return new UserAuth();
		}

		public UserAuth Get(UserAuthRequest request)
		{
			IUserAuthRepository repository = AppHost.UserRepository;

			UserAuth userAuth = repository.GetUserAuth(this.GetSession().UserAuthId);

			return userAuth;
		}

		public bool Post(UserExistRequest request)
		{
			var repository = AppHost.UserRepository;
			return repository.GetUserAuthByUserName(request.UserName) != null;
		}

		public LocalizationDictionaryResponse Get(LocalizationDictionaryRequest request)
		{
			var repository = AppHost.UserRepository;
			return repository.GetLocalizationDictionary(request.Locale);
		}

		public LocalizationDictionaryResponse Post(LocalizationDictionaryRequest request)
		{
			return PutPostLocalizationDictionary(request);
		}

		public LocalizationDictionaryResponse Put(LocalizationDictionaryRequest request)
		{
			return PutPostLocalizationDictionary(request);
		}

		#endregion Public Methods

		#region Private Methods

		private LocalizationDictionaryResponse PutPostLocalizationDictionary(LocalizationDictionaryRequest request)
		{
			var repository = AppHost.UserRepository;
			return repository.InsertLocalizationDictionary(request);
		}

		#endregion Private Methods
	}
}