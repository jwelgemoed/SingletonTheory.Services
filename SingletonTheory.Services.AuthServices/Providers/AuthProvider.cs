using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Providers
{
	public class AuthProvider : CredentialsAuthProvider
	{
		#region Fields & Properties

		private List<string> _blacklist = new List<string>();

		#endregion Fields & Properties

		#region Override Methods

		public override bool TryAuthenticate(ServiceStack.ServiceInterface.IServiceBase authService, string userName, string password)
		{
			bool auth = base.TryAuthenticate(authService, userName, password);

			return auth;
		}

		protected override void SaveUserAuth(ServiceStack.ServiceInterface.IServiceBase authService, IAuthSession session, IUserAuthRepository authRepo, IOAuthTokens tokens)
		{
			base.SaveUserAuth(authService, session, authRepo, tokens);
		}

		public override void OnSaveUserAuth(ServiceStack.ServiceInterface.IServiceBase authService, IAuthSession session)
		{
			base.OnSaveUserAuth(authService, session);
		}

		public override void OnFailedAuthentication(IAuthSession session, ServiceStack.ServiceHost.IHttpRequest httpReq, ServiceStack.ServiceHost.IHttpResponse httpRes)
		{
			base.OnFailedAuthentication(session, httpReq, httpRes);
		}

		public override void OnAuthenticated(ServiceStack.ServiceInterface.IServiceBase authService, IAuthSession session, IOAuthTokens tokens, System.Collections.Generic.Dictionary<string, string> authInfo)
		{
			base.OnAuthenticated(authService, session, tokens, authInfo);
		}

		protected override void LoadUserAuthInfo(AuthUserSession userSession, IOAuthTokens tokens, System.Collections.Generic.Dictionary<string, string> authInfo)
		{
			base.LoadUserAuthInfo(userSession, tokens, authInfo);
		}

		public override bool IsAuthorized(IAuthSession session, IOAuthTokens tokens, Auth request = null)
		{
			if (_blacklist.Contains(session.Id))
				return false;

			bool isAuthorized = base.IsAuthorized(session, tokens, request);

			return isAuthorized;
		}

		public override object Authenticate(ServiceStack.ServiceInterface.IServiceBase authService, IAuthSession session, Auth request)
		{
			object auth = base.Authenticate(authService, session, request);

			return auth;
		}

		public override object Logout(ServiceStack.ServiceInterface.IServiceBase service, Auth request)
		{
			_blacklist.Add(service.GetSessionId());

			object logout = base.Logout(service, request);

			return logout;
		}

		#endregion Override Methods
	}
}