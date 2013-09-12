using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Services.AuthServices.Providers;
using System.Collections.Generic;
using SSAuthInterfaces = ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices
{
	public class HelloAppHost : AppHostBase
	{
		#region Constants

		private const string UserName = "user";
		private const string Password = "123";

		#endregion Constants

		#region Fields & Properties

		private SSAuthInterfaces.InMemoryAuthRepository _userRepository;

		#endregion Fields & Properties

		/// <summary>
		/// Tell Service Stack the name of your application and where to find your web services
		/// </summary>
		public HelloAppHost() : base("Hello Web Services", typeof(HelloService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
			//register any dependencies your services use, e.g:
			//container.Register<ICacheClient>(new MemoryCacheClient());

			AddPlugins();

			container.Register<ICacheClient>(new MemoryCacheClient());
			_userRepository = new SSAuthInterfaces.InMemoryAuthRepository();
			container.Register<SSAuthInterfaces.IUserAuthRepository>(_userRepository);

			//The IUserAuthRepository is used to store the user credentials etc.
			//Implement this interface to adjust it to your application's data storage.
			CreateUser(1, UserName, null, Password, new List<string> { "user" }, new List<string> { "ThePermission" });
			//CreateUser(2, UserNameWithSessionRedirect, null, PasswordForSessionRedirect);
			//CreateUser(3, null, EmailBasedUsername, PasswordForEmailBasedAccount);
		}

		private void CreateUser(int id, string username, string email, string password, List<string> roles = null, List<string> permissions = null)
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

			_userRepository.CreateUserAuth(userAuth, password);
		}

		private void AddPlugins()
		{
			AppSettings appSettings = new AppSettings();
			SSAuthInterfaces.AuthUserSession authUserSession = new SSAuthInterfaces.AuthUserSession();
			AuthProvider authProvider = new AuthProvider();
			//CredentialsAuthProvider credentialsAuthProvider = new CredentialsAuthProvider(appSettings);
			SSAuthInterfaces.IAuthProvider[] authProviders = new SSAuthInterfaces.IAuthProvider[] { authProvider }; //, basicAuthProvider };

			Plugins.Add(new AuthFeature(() => authUserSession, authProviders) { }); //HtmlRedirect = "/login" }); // 
			Plugins.Add(new RegistrationFeature());
		}
	}
}