using System;
using System.Xml.Serialization;
using MongoDB.Driver;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Data;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.Providers;
using System.Collections.Generic;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SingletonTheory.Services.AuthServices.Validations;
using MongoAuthInterfaces = ServiceStack.ServiceInterface.Auth;
using SSAuthInterfaces = ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Host
{
	public class AppHost : AppHostBase
	{
		#region Constants

		private const string UserName = "user";
		private const string AdminUserName = "admin";
		private const string Password = "123";
		private LocalizationDictionaryRequest LocaleUSFile = new LocalizationDictionaryRequest()
		{
			Locale = "en-US",
			LocalizationDictionary = new List<LocalizationItem>()
			                         {
				                         new LocalizationItem()
				                         {
					                         Key = "_TestTitle_", 
																	 Value = "This comes from the English US file.", 
																	 Description = "Test title description for US"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LogInAnchor_", 
																	 Value = "Log In", 
																	 Description = "Main Nav Bar Login Item"
				                         }
			                         }
		};
		private LocalizationDictionaryRequest LocaleNLFile = new LocalizationDictionaryRequest()
		{
			Locale = "nl-nl",
			LocalizationDictionary = new List<LocalizationItem>()
			                         {
				                         new LocalizationItem()
				                         {
					                         Key = "_TestTitle_", 
																	 Value = "Deze komen van de Nederlandse bestand.", 
																	 Description = "Test hoofd voor Nederlands"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LogInAnchor_", 
																	 Value = "Inloggen", 
																	 Description = "Hoofd navigatie inloggen item."
				                         }
			                         }
		};
		private LocalizationDictionaryRequest LocaleDefaultFile = new LocalizationDictionaryRequest()
		{
			Locale = "default",
			LocalizationDictionary = new List<LocalizationItem>()
			                         {
				                         new LocalizationItem()
				                         {
					                         Key = "_TestTitle_", 
																	 Value = "This comes from the Default file.", 
																	 Description = "Test title description for default"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LogInAnchor_", 
																	 Value = "Log In", 
																	 Description = "Main Nav Bar Login Item"
				                         }
			                         }
		};

		#endregion Constants

		#region Fields & Properties

		private static ICustomUserAuthRepository _userRepository;

		public static ICustomUserAuthRepository UserRepository
		{
			get { return _userRepository; }
			set { _userRepository = value; }
		}

		#endregion Fields & Properties

		#region Constructors

		/// <summary>
		/// Tell Service Stack the name of your application and where to find your web services
		/// </summary>
		public AppHost() : base("Singleton Theory Auth Services", typeof(AuthService).Assembly) { }

		#endregion Constructors

		#region Override Methods

		public override void Configure(Funq.Container container)
		{
			AddPlugins();

			container.Register<ICacheClient>(new MemoryCacheClient());
			_userRepository = GetRepositoryProvider();
			container.Register<SSAuthInterfaces.IUserAuthRepository>(_userRepository);

			RegisterValidations(container);

			CreateUser(0, UserName, null, Password, new List<string> { "user" }, new List<string> { "ThePermission" });
			CreateUser(0, AdminUserName, null, Password, new List<string> { "admin" }, new List<string> { "ThePermission" });
			CreateTestingLanguageFiles();
		}

		private void RegisterValidations(Funq.Container container)
		{
			container.RegisterValidators(typeof(UserRequestValidator).Assembly);
		}

		#endregion Override Methods

		#region Static Methods

		private static ICustomUserAuthRepository GetRepositoryProvider()
		{
			// Enable the following lines to enable MongoDB
			MongoDatabase userDatabase = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, ConfigSettings.MongoUserDatabaseName);

			return new CustomMongoDBAuthRepository(userDatabase, true);
			//return new SSAuthInterfaces.InMemoryAuthRepository();
		}

		#endregion Static Methods

		#region Private Methods

		private void CreateTestingLanguageFiles()
		{
			_userRepository.InsertLocalizationDictionary(LocaleDefaultFile);
			_userRepository.InsertLocalizationDictionary(LocaleUSFile);
			_userRepository.InsertLocalizationDictionary(LocaleNLFile);
		}

		private void CreateUser(int id, string username, string email, string password, List<string> roles = null, List<string> permissions = null)
		{
			string hash;
			string salt;
			new SSAuthInterfaces.SaltedHash().GetHashAndSaltString(password, out hash, out salt);
			Dictionary<string, string> meta = new Dictionary<string, string>();
			meta.Add("Active", true.ToString());
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
				_userRepository.CreateUserAuth(userAuth, password);
			}
			catch (Exception ex)
			{
				var x = ex.Message;
			}
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
			Plugins.Add(new ValidationFeature());
		}

		#endregion Private Methods
	}
}