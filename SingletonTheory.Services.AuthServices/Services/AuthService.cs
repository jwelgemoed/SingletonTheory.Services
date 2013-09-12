using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class AuthService : Service
	{
		#region Public Methods

		public UserAuth Get(CurrentUserRequest request)
		{
			IAuthSession session = this.GetSession();
			UserService userService = new UserService();
			List<UserAuth> response = userService.Get(new UserRequest() { UserName = session.UserName });
			if (response.Count != 0)
				return response[0];

			return new UserAuth();
		}

		#endregion Public Methods
	}
}