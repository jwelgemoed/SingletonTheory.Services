using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Host;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class UserService : Service
	{
		public List<UserAuth> Get(UserRequest request)
		{
			ICustomUserAuthRepository repository = AppHost.UserRepository;
			List<UserAuth> response = new List<UserAuth>();

			if (request.Id == 0)
			{
				// Get all users
				response = repository.GetAllUserAuths();
			}
			else
			{
				// Get user with id
				UserAuth userAuth = repository.GetUserAuth(request.Id.ToString(CultureInfo.InvariantCulture));

				response.Add(userAuth);
			}

			return response;
		}

		public UserAuth Put(UserRequest request)
		{
			ICustomUserAuthRepository repository = AppHost.UserRepository;
			UserAuth userToUpdate = repository.GetUserAuth(request.Id.ToString(CultureInfo.InvariantCulture));
			if (userToUpdate == null)
				throw HttpError.NotFound("User not found in User Database.");

			Dictionary<string, string> meta = new Dictionary<string, string>();

			meta.Add("Active", request.Active.ToString());
			meta.Add("Language", request.Language);

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
			var meta = new Dictionary<string, string>();

			meta.Add("Active", request.Active.ToString());
			meta.Add("Language", request.Language);

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
	}
}