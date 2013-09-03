﻿using System;
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
#region items
				                         new LocalizationItem()
				                         {
					                         Key = "_MainTitle_", 
																	 Value = "Singleton Theory Auth.", 
																	 Description = "Main app title."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LogInHeading_", 
																	 Value = "Log In", 
																	 Description = "Log in heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_HomeAnchor_", 
																	 Value = "Home", 
																	 Description = "Main Nav Bar Home Item"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AdminActionsAnchor_", 
																	 Value = "Administrator Actions", 
																	 Description = "Main Nav Bar Admin Actions Item"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AdministrateUsersHeading_", 
																	 Value = "Adminstrate Users", 
																	 Description = "Administrate Users heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LogOutAnchor_", 
																	 Value = "Log Out", 
																	 Description = "Main Nav Bar Logout Item"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UserWelcomeMessage_", 
																	 Value = "Welcome", 
																	 Description = "Welcome message for logged in user."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UserNameHeading_", 
																	 Value = "Username", 
																	 Description = "User name heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UserNameMessage_", 
																	 Value = "Enter a unique username", 
																	 Description = "User name message for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_PasswordHeading_", 
																	 Value = "Password", 
																	 Description = "Password heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_RememberMeHeading_", 
																	 Value = "Remember Me", 
																	 Description = "Rememberme heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_DashboardGreeting_", 
																	 Value = "Welcome to the Dashboard.", 
																	 Description = "Heading for dashboard screen."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_DashboardSplash_", 
																	 Value = "This view will house the dashboard, initially visible to all logged in users.", 
																	 Description = "Content holder for dashboard screen."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AddHeading_", 
																	 Value = "Add", 
																	 Description = "Add heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_SearchMessage_", 
																	 Value = "Start typing to Search", 
																	 Description = "Search message for re-use"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_RefreshHeading_", 
																	 Value = "Refresh", 
																	 Description = "Refresh heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_EditHeading_", 
																	 Value = "Edit", 
																	 Description = "Edit heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_IdentityHeading_", 
																	 Value = "Id", 
																	 Description = "Identity heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_RoleHeading_", 
																	 Value = "Role", 
																	 Description = "Role heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_ActiveHeading_", 
																	 Value = "Active", 
																	 Description = "Active heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AddUserHeading_", 
																	 Value = "Add User", 
																	 Description = "Add user heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_EditUserHeading_", 
																	 Value = "Edit User", 
																	 Description = "Edit user heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_SaveHeading_", 
																	 Value = "Save", 
																	 Description = "Save heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UpdateHeading_", 
																	 Value = "Update", 
																	 Description = "Update heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_CancelHeading_", 
																	 Value = "Cancel", 
																	 Description = "Cancel heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AllUsersHeading_", 
																	 Value = "All Users", 
																	 Description = "All users heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_ActiveUsersHeading_", 
																	 Value = "Active Users", 
																	 Description = "Active users heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_InactiveUsersHeading_", 
																	 Value = "In-Active Users", 
																	 Description = "In-Active users heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LanguageHeading_", 
																	 Value = "Language", 
																	 Description = "Language heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_EnglishHeading_", 
																	 Value = "English", 
																	 Description = "English heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_DutchHeading_", 
																	 Value = "Dutch", 
																	 Description = "Dutch heading for re-use."
				                         }
#endregion items
			                         }
		};
		private LocalizationDictionaryRequest LocaleNLFile = new LocalizationDictionaryRequest()
		{
			Locale = "nl-nl",
			LocalizationDictionary = new List<LocalizationItem>()
			                         {
#region items
				                         new LocalizationItem()
				                         {
					                         Key = "_MainTitle_", 
																	 Value = "Singleton Theory Toegangsapplicatie.", 
																	 Description = "Main app title."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LogInHeading_", 
																	 Value = "Inloggen", 
																	 Description = "Hoofd voor inloggen."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_HomeAnchor_", 
																	 Value = "Thuis", 
																	 Description = "Hoofd navigatie thuis item."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AdminActionsAnchor_", 
																	 Value = "Beheerder Acties", 
																	 Description = "Hoofd navigatie beheerder item."
				                         },
																  new LocalizationItem()
				                         {
																	 Key = "_AdministrateUsersHeading_", 
																	 Value = "Beheer Gebruikers", 
																	 Description = "Hoofd voor gebruikersbeheer."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LogOutAnchor_", 
																	 Value = "Uitloggen", 
																	 Description = "Hoofd navigatie uitloggen item."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UserWelcomeMessage_", 
																	 Value = "Welkom", 
																	 Description = "Welkom boodschap voor huidige gebruiker."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UserNameHeading_", 
																	 Value = "Gebruikersnaam", 
																	 Description = "Gebruikersnaam hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UserNameMessage_", 
																	 Value = "Unieke gebruikersnaam AUB", 
																	 Description = "Gebruikersnaam boodschap voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_PasswordHeading_", 
																	 Value = "Wachtwoord", 
																	 Description = "Wachtwoord hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_RememberMeHeading_", 
																	 Value = "Aangemeld Blijven", 
																	 Description = "Aangemeld blijven hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_DashboardGreeting_", 
																	 Value = "Welkom bij het Dashboard", 
																	 Description = "Heading for dashboard screen."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_DashboardSplash_", 
																	 Value = "Deze pagina zal het dashboard huisvesten, aanvankelijk zichtbaar voor alle gebruikers die ingelogd zijn.", 
																	 Description = "Content holder for dashboard screen."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AddHeading_", 
																	 Value = "Toevoegen", 
																	 Description = "Toevoegen hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_SearchMessage_", 
																	 Value = "Typ om te zoeken", 
																	 Description = "Zoeken hoofd voor hergebruik"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_RefreshHeading_", 
																	 Value = "Verversen", 
																	 Description = "Verversen hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_EditHeading_", 
																	 Value = "Redigeren", 
																	 Description = "Redigeren hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_IdentityHeading_", 
																	 Value = "Id", 
																	 Description = "Identiteit hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_RoleHeading_", 
																	 Value = "Rol", 
																	 Description = "Rol hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_ActiveHeading_", 
																	 Value = "Aktief", 
																	 Description = "Aktief hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AddUserHeading_", 
																	 Value = "Gebruiker Toevoegen", 
																	 Description = "Gebruiker toevoegen hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_EditUserHeading_", 
																	 Value = "Gebruiker Redigeren", 
																	 Description = "Gebruiker redigeren hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_SaveHeading_", 
																	 Value = "Opslaan", 
																	 Description = "Opslaan hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UpdateHeading_", 
																	 Value = "Opslaan", 
																	 Description = "Opdateren hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_CancelHeading_", 
																	 Value = "Anuleer", 
																	 Description = "Anuleer hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AllUsersHeading_", 
																	 Value = "Alle Gebruikers", 
																	 Description = "Alle gebruikers hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_ActiveUsersHeading_", 
																	 Value = "Aktieve Gebruikers", 
																	 Description = "Aktieve gebruikers hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_InactiveUsersHeading_", 
																	 Value = "Onaktieve Gebruikers", 
																	 Description = "Onaktieve gebruikers hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LanguageHeading_", 
																	 Value = "Taal", 
																	 Description = "Taal hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_EnglishHeading_", 
																	 Value = "Engels", 
																	 Description = "Engels hoofd voor hergebruik."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_DutchHeading_", 
																	 Value = "Nederlands", 
																	 Description = "Nederlands hoofd voor hergebruik."
				                         }
#endregion
			                         }
		};
		private LocalizationDictionaryRequest LocaleDefaultFile = new LocalizationDictionaryRequest()
		{
			Locale = "default",
			LocalizationDictionary = new List<LocalizationItem>()
			                         {
#region items
				                         new LocalizationItem()
				                         {
					                         Key = "_MainTitle_", 
																	 Value = "Singleton Theory Auth.", 
																	 Description = "Main app title."
				                         },
																  new LocalizationItem()
				                         {
					                          Key = "_LogInHeading_", 
																	 Value = "Log In", 
																	 Description = "Log in heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_HomeAnchor_", 
																	 Value = "Home", 
																	 Description = "Main Nav Bar Home Item"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AdminActionsAnchor_", 
																	 Value = "Administrator Actions", 
																	 Description = "Main Nav Bar Admin Actions Item"
				                         },
																  new LocalizationItem()
				                         {
					                          Key = "_AdministrateUsersHeading_", 
																	 Value = "Adminstrate Users", 
																	 Description = "Administrate Users heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LogOutAnchor_", 
																	 Value = "Log Out", 
																	 Description = "Main Nav Bar Logout Item"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UserWelcomeMessage_", 
																	 Value = "Welcome", 
																	 Description = "Welcome message for logged in user."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UserNameHeading_", 
																	 Value = "Username", 
																	 Description = "User name heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UserNameMessage_", 
																	 Value = "Enter a unique username", 
																	 Description = "User name message for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_PasswordHeading_", 
																	 Value = "Password", 
																	 Description = "Password heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_RememberMeHeading_", 
																	 Value = "Remember Me", 
																	 Description = "Rememberme heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_DashboardGreeting_", 
																	 Value = "Welcome to the Dashboard.", 
																	 Description = "Heading for dashboard screen."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_DashboardSplash_", 
																	 Value = "This view will house the dashboard, initially visible to all logged in users.", 
																	 Description = "Content holder for dashboard screen."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AddHeading_", 
																	 Value = "Add", 
																	 Description = "Add heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_SearchMessage_", 
																	 Value = "Start typing to Search", 
																	 Description = "Search message for re-use"
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_RefreshHeading_", 
																	 Value = "Refresh", 
																	 Description = "Refresh heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_EditHeading_", 
																	 Value = "Edit", 
																	 Description = "Edit heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_IdentityHeading_", 
																	 Value = "Id", 
																	 Description = "Identity heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_RoleHeading_", 
																	 Value = "Role", 
																	 Description = "Role heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_ActiveHeading_", 
																	 Value = "Active", 
																	 Description = "Active heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AddUserHeading_", 
																	 Value = "Add User", 
																	 Description = "Add user heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_EditUserHeading_", 
																	 Value = "Edit User", 
																	 Description = "Edit user heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_SaveHeading_", 
																	 Value = "Save", 
																	 Description = "Save heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_UpdateHeading_", 
																	 Value = "Update", 
																	 Description = "Update heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_CancelHeading_", 
																	 Value = "Cancel", 
																	 Description = "Cancel heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_AllUsersHeading_", 
																	 Value = "All Users", 
																	 Description = "All users heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_ActiveUsersHeading_", 
																	 Value = "Active Users", 
																	 Description = "Active users heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_InactiveUsersHeading_", 
																	 Value = "In-Active Users", 
																	 Description = "In-Active users heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_LanguageHeading_", 
																	 Value = "Language", 
																	 Description = "Language heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_EnglishHeading_", 
																	 Value = "English", 
																	 Description = "English heading for re-use."
				                         },
																  new LocalizationItem()
				                         {
					                         Key = "_DutchHeading_", 
																	 Value = "Dutch", 
																	 Description = "Dutch heading for re-use."
				                         }
#endregion
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
			meta.Add("Language","en-US");
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