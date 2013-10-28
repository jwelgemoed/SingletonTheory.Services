using Funq;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using System.Collections.Generic;

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

		private static UserRepository GetUserRepository()
		{
			Container container = EndpointHost.Config.ServiceManager.Container;
			return container.Resolve<UserRepository>();
		}

		#endregion Private Methods
	}
}