using ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Providers
{
	public class AuthProvider : CredentialsAuthProvider
	{
		#region Override Methods

		public override bool TryAuthenticate(ServiceStack.ServiceInterface.IServiceBase authService, string userName, string password)
		{
			return base.TryAuthenticate(authService, userName, password);
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
			return base.IsAuthorized(session, tokens, request);
		}

		public override object Authenticate(ServiceStack.ServiceInterface.IServiceBase authService, IAuthSession session, Auth request)
		{
			return base.Authenticate(authService, session, request);
		}

		public override object Logout(ServiceStack.ServiceInterface.IServiceBase service, Auth request)
		{
			return base.Logout(service, request);
		}

		#endregion Override Methods
	}
}