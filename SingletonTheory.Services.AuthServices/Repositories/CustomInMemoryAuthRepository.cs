using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Interfaces;

namespace SingletonTheory.Services.AuthServices.Repositories
{
    public class CustomInMemoryAuthRepository : InMemoryAuthRepository, ICustomUserAuthRepository
    {
        public override void LoadUserAuth(IAuthSession session, IOAuthTokens tokens)
        {
            base.LoadUserAuth(session, tokens);
        }

        public List<UserAuth> GetAllUserAuths()
        {
            throw new NotImplementedException();
        }
    }
}