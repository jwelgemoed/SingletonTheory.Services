using Funq;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using System;
using System.Collections.Generic;
using SSAuthInterfaces = ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Data
{
	public static class UserData
	{
		#region Constants

		private const string UserName = "user";
		private const string AdminUserName = "admin";
		private const string DutchUserName = "nlgebruiker";
		private const string Password = "123";

		#endregion Constants

		#region Public Methods

		public static void CreateUsers()
		{
			ClearUserAuths();
			//CreateUserAuth(0, UserName, null, Password, "en-US", new List<string> { "user" }, new List<string> { "ThePermission" });
			//CreateUserAuth(0, AdminUserName, null, Password, "en-US", new List<string> { "admin" }, new List<string> { "ThePermission" });
			//CreateUserAuth(0, DutchUserName, null, Password, "nl-nl", new List<string> { "admin" }, new List<string> { "ThePermission" });

			ClearUsers();
			CreateUser(0, UserName, null, Password, "en-US", new List<int> { 2 }, new List<string> { "ThePermission" });
			CreateUser(0, AdminUserName, null, Password, "en-US", new List<int> { 1 }, new List<string> { "ThePermission" });
			CreateUser(0, DutchUserName, null, Password, "nl-nl", new List<int> { 1 }, new List<string> { "ThePermission" });
		}

		#endregion Public Methods

		#region Private Methods

		private static void ClearUsers()
		{
			UserRepository repository = GetUserRepository();
			repository.ClearCollection();
		}

		private static void ClearUserAuths()
		{
			UserAuthRepository repository = GetUserAuthRepository() as UserAuthRepository;
			repository.Clear();
		}

		private static void CreateUser(int id, string username, string email, string password, string language, List<int> roles = null, List<string> permissions = null)
		{
			UserRepository repository = GetUserRepository();
			UserEntity entity = new UserEntity();

			entity.UserName = username;
			entity.PasswordHash = password;
			entity.Language = language;
			entity.Active = true;
			entity.Roles = roles;
			entity.Permissions = permissions;
			entity.Language = language;

			repository.Create(entity);
		}

		private static void CreateUserAuth(int id, string username, string email, string password, string language, List<string> roles = null, List<string> permissions = null)
		{
			SSAuthInterfaces.IUserAuthRepository repository = GetUserAuthRepository();
			string hash;
			string salt;
			new SSAuthInterfaces.SaltedHash().GetHashAndSaltString(password, out hash, out salt);
			Dictionary<string, string> meta = new Dictionary<string, string>();
			meta.Add("Active", true.ToString());
			meta.Add("Language", language);
			SSAuthInterfaces.UserAuth userAuth = new SSAuthInterfaces.UserAuth
			{
				Id = id,
				DisplayName = "DisplayName",
				//Email = email ?? "as@if{0}.com".Fmt(id),
				UserName = username,
				FirstName = "FirstName",
				LastName = "LastName",
				PasswordHash = hash,
				Salt = salt,
				Roles = roles,
				Permissions = permissions,
				Meta = meta
			};

			try
			{
				repository.CreateUserAuth(userAuth, password);
			}
			catch (Exception ex)
			{
				var x = ex.Message;
			}
		}

		private static SSAuthInterfaces.IUserAuthRepository GetUserAuthRepository()
		{
			Container container = EndpointHost.Config.ServiceManager.Container;
			return container.Resolve<SSAuthInterfaces.IUserAuthRepository>();
		}

		private static UserRepository GetUserRepository()
		{
			Container container = EndpointHost.Config.ServiceManager.Container;
			return container.Resolve<UserRepository>();
		}

		#endregion Private Methods
	}
}