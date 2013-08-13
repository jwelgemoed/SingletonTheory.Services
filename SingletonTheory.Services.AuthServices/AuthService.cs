using Bridge.AuthenticationServices.TransferObjects;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

namespace Bridge.AuthenticationServices
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

		#endregion Public Methods
	}
}