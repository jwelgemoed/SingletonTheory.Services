using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
    public class UserRequest : IReturn<UserAuth>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
    }
}