using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Repositories
{
    public class CustomInMemoryAuthRepository : InMemoryAuthRepository
    {
        public override void LoadUserAuth(IAuthSession session, IOAuthTokens tokens)
        {
            base.LoadUserAuth(session, tokens);
        }
    }
}