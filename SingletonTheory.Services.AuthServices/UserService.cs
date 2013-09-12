using System.Collections.Generic;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Text;
using SingletonTheory.Services.AuthServices.Host;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SSAuthInterfaces = ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices
{
    [Authenticate]
    public class UserService : Service
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
            return GenericRepository.GetList<UserAuth>("UserDatabase", "UserAuth");
        }

        public UserAuth Post(UserRequest request)
        {
            UserAuth userAuth = CreateUserAuth(GenericRepository.GetMaxId<UserAuth>("UserDatabase", "UserAuth") + 1, request.UserName, "bla@bla.com", request.Password,
                new List<string>() { request.Role }, new List<string> { "ThePermission" });

            userAuth = GenericRepository.Add("UserDatabase", "UserAuth", userAuth);

            return userAuth;
        }

        #endregion Public Methods

        #region Private Methods

        private UserAuth CreateUserAuth(int id, string username, string email, string password, List<string> roles, List<string> permissions = null)
        {
            string hash;
            string salt;
            new SSAuthInterfaces.SaltedHash().GetHashAndSaltString(password, out hash, out salt);

            SSAuthInterfaces.UserAuth userAuth = new SSAuthInterfaces.UserAuth
            {
                Id = id,
                DisplayName = "DisplayName",
                Email = email ?? "as@if{0}.com".Fmt(id),
                UserName = username,
                FirstName = "FirstName",
                LastName = "LastName",
                PasswordHash = hash,
                Salt = salt,
                Roles = roles,
                Permissions = permissions
            };

            return userAuth;
        }

        #endregion  Private Methods
    }

}