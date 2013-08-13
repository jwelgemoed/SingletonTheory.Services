using ServiceStack.ServiceInterface.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class AuthRepository : InMemoryAuthRepository
	{
		public override void LoadUserAuth(IAuthSession session, IOAuthTokens tokens)
		{
			base.LoadUserAuth(session, tokens);
		}
	}
}