using System;
using System.Collections.Generic;
using System.Globalization;
using ServiceStack.Common.Web;
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

        public UserAuth Get(UserRequest request)
        {
            ICustomUserAuthRepository repository = AppHost.UserRepository;
            return repository.GetUserAuth(request.Id.ToString(CultureInfo.InvariantCulture));
        }

        public UserAuth Put(UserRequest request)
        {
            ICustomUserAuthRepository repository = AppHost.UserRepository;
            UserAuth userToUpdate = repository.GetUserAuth(request.Id.ToString(CultureInfo.InvariantCulture));
            if (userToUpdate == null)
                throw HttpError.NotFound("User not found in User Database.");
            Dictionary<string, string> meta = new Dictionary<string, string>();
            meta.Add("Active", request.Active.ToString());
            userToUpdate.Meta = meta;
            userToUpdate.Roles = new List<string> { request.Role };
            repository.SaveUserAuth(userToUpdate);
            return userToUpdate;
        }

        public UserAuth Post(UserRequest request)
        {
            string hash;
            string salt;
            new SaltedHash().GetHashAndSaltString(request.Password, out hash, out salt);
            var meta = new Dictionary<string, string> { { "Active", request.Active.ToString() } };
            var userAuth = new UserAuth
            {
                Id = 0,
                UserName = request.UserName,
                PasswordHash = hash,
                Salt = salt,
                Roles = new List<string> { request.Role },
                Meta = meta
            };
            var repository = AppHost.UserRepository;
            try
            {
                repository.CreateUserAuth(userAuth, request.Password);
            }
            catch (ArgumentException ex)
            {
                throw HttpError.Conflict(ex.Message);
            }
            return userAuth;
        }

        public List<UserAuth> Get(UserListRequest request)
        {
            var repository = AppHost.UserRepository;
            return repository.GetAllUserAuths();
        }

        public bool Post(UserExistRequest request)
        {
            var repository = AppHost.UserRepository;
            return repository.GetUserAuthByUserName(request.UserName) != null;
        }

	    public LocalizationDictionaryResponse Get(LocalizationDictionaryRequest request)
	    {
				var repository = AppHost.UserRepository;
				return repository.GetLocalizationDictionary(request.Locale);
	    }

	    public LocalizationDictionaryResponse Post(LocalizationDictionaryRequest request)
	    {
				var x = new LocalizationDictionaryResponse();
				return x;
	    }

	    public LocalizationDictionaryResponse Put(LocalizationDictionaryRequest request)
	    {
				var x = new LocalizationDictionaryResponse();
				return x;
	    }

	    #endregion Public Methods
    }
}