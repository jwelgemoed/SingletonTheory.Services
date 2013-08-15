using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
    public class UserListRequest: IReturn<List<UserAuth>>
    {
    }
}