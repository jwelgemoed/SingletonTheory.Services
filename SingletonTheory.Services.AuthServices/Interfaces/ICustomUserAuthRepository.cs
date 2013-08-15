using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Interfaces
{
    public interface ICustomUserAuthRepository : IUserAuthRepository
    {
        List<UserAuth> GetAllUserAuths();
    }
}