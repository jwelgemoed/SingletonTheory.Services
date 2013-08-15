using System.Collections.Generic;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Host;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices
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

        public UserAuth Get(UserAuthRequest request)
        {
            IUserAuthRepository repository = AppHost.UserRepository;

            UserAuth userAuth = repository.GetUserAuth(this.GetSession().UserAuthId);

            return userAuth;
        }

        public List<UserAuth> Get(UserListRequest request)
        {
            IUserAuthRepository repository = AppHost.UserRepository;
            return ((CustomMongoDBAuthRepository)repository).GetAllUserAuths();
        }

        #endregion Public Methods
    }
}