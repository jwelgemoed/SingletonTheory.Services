using System;
using System.Collections.Generic;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Text;
using SingletonTheory.Services.AuthServices.Host;
using SingletonTheory.Services.AuthServices.Interfaces;
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

        public UserAuth Post(UserRequest request)
        {
            string hash;
            string salt;
            new SaltedHash().GetHashAndSaltString(request.Password, out hash, out salt);
            var userAuth = new UserAuth
            {
                Id = 0,
                UserName = request.UserName,
                PasswordHash = hash,
                Salt = salt,
                Roles = new List<string> { request.Role }
            };
            try
            {
                ICustomUserAuthRepository repository = (ICustomUserAuthRepository)AppHost.UserRepository;
                repository.CreateUserAuth(userAuth, request.Password);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
            return userAuth;
        }

        public List<UserAuth> Get(UserListRequest request)
        {
            ICustomUserAuthRepository repository = (ICustomUserAuthRepository)AppHost.UserRepository;
            return repository.GetAllUserAuths();
        }

        #endregion Public Methods
    }
}