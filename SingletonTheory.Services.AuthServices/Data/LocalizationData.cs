using Funq;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Data
{
	public static class LocalizationData
	{
		private static LocalizationCollectionEntity LocaleUSFile = new LocalizationCollectionEntity()
		{
			Locale = "en-US",
			LocalizationItems = new List<LocalizationEntity>()
			                         {
																#region items
				                         new LocalizationEntity()
				                         {
					                         Key = "_MainTitle_", 
																	 Value = "Singleton Theory Auth.", 
																	 Description = "Main app title."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_LogInHeading_", 
																	 Value = "Log In", 
																	 Description = "Log in heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_HomeAnchor_", 
																	 Value = "Home", 
																	 Description = "Main Nav Bar Home Item"
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AdminActionsAnchor_", 
																	 Value = "Administrator Actions", 
																	 Description = "Main Nav Bar Admin Actions Item"
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AdministrateUsersHeading_", 
																	 Value = "Adminstrate Users", 
																	 Description = "Administrate Users heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_LogOutAnchor_", 
																	 Value = "Log Out", 
																	 Description = "Main Nav Bar Logout Item"
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UserWelcomeMessage_", 
																	 Value = "Welcome", 
																	 Description = "Welcome message for logged in user."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UserNameHeading_", 
																	 Value = "Username", 
																	 Description = "User name heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UserNameMessage_", 
																	 Value = "Enter a unique username", 
																	 Description = "User name message for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_PasswordHeading_", 
																	 Value = "Password", 
																	 Description = "Password heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_RememberMeHeading_", 
																	 Value = "Remember Me", 
																	 Description = "Rememberme heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DashboardGreeting_", 
																	 Value = "Welcome to the Dashboard.", 
																	 Description = "Heading for dashboard screen."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DashboardSplash_", 
																	 Value = "This view will house the dashboard, initially visible to all logged in users.", 
																	 Description = "Content holder for dashboard screen."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AddHeading_", 
																	 Value = "Add", 
																	 Description = "Add heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_SearchMessage_", 
																	 Value = "Start typing to Search", 
																	 Description = "Search message for re-use"
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_RefreshHeading_", 
																	 Value = "Refresh", 
																	 Description = "Refresh heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_EditHeading_", 
																	 Value = "Edit", 
																	 Description = "Edit heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_IdentityHeading_", 
																	 Value = "Id", 
																	 Description = "Identity heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_RoleHeading_", 
																	 Value = "Role", 
																	 Description = "Role heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_ActiveHeading_", 
																	 Value = "Active", 
																	 Description = "Active heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AddUserHeading_", 
																	 Value = "Add User", 
																	 Description = "Add user heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_EditUserHeading_", 
																	 Value = "Edit User", 
																	 Description = "Edit user heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_SaveHeading_", 
																	 Value = "Save", 
																	 Description = "Save heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UpdateHeading_", 
																	 Value = "Update", 
																	 Description = "Update heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_CancelHeading_", 
																	 Value = "Cancel", 
																	 Description = "Cancel heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AllUsersHeading_", 
																	 Value = "All Users", 
																	 Description = "All users heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_ActiveUsersHeading_", 
																	 Value = "Active Users", 
																	 Description = "Active users heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_InactiveUsersHeading_", 
																	 Value = "In-Active Users", 
																	 Description = "In-Active users heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_LanguageHeading_", 
																	 Value = "Language", 
																	 Description = "Language heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_EnglishHeading_", 
																	 Value = "English", 
																	 Description = "English heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DutchHeading_", 
																	 Value = "Dutch", 
																	 Description = "Dutch heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AuthAdminHeading_", 
																	 Value = "Authorization Administration", 
																	 Description = "AuthAdmin heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_ElementHeading_", 
																	 Value = "Permission Element", 
																	 Description = "Element heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DomainPermissionHeading_", 
																	 Value = "Domain Permission", 
																	 Description = "Domain Permission heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_FunctionalPermissionHeading_", 
																	 Value = "Functional Permission", 
																	 Description = "Function Permission heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_PermissionHeading_", 
																	 Value = "Permission", 
																	 Description = "Permission heading for re-use."
				                         }
																 
#endregion items
			                         }
		};
		private static LocalizationCollectionEntity LocaleNLFile = new LocalizationCollectionEntity()
		{
			Locale = "nl-nl",
			LocalizationItems = new List<LocalizationEntity>()
			                         {
#region items
				                         new LocalizationEntity()
				                         {
					                         Key = "_MainTitle_", 
																	 Value = "Singleton Theory Toegangsapplicatie.", 
																	 Description = "Main app title."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_LogInHeading_", 
																	 Value = "Inloggen", 
																	 Description = "Hoofd voor inloggen."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_HomeAnchor_", 
																	 Value = "Thuis", 
																	 Description = "Hoofd navigatie thuis item."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AdminActionsAnchor_", 
																	 Value = "Beheerder Acties", 
																	 Description = "Hoofd navigatie beheerder item."
				                         },
																  new LocalizationEntity()
				                         {
																	 Key = "_AdministrateUsersHeading_", 
																	 Value = "Beheer Gebruikers", 
																	 Description = "Hoofd voor gebruikersbeheer."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_LogOutAnchor_", 
																	 Value = "Uitloggen", 
																	 Description = "Hoofd navigatie uitloggen item."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UserWelcomeMessage_", 
																	 Value = "Welkom", 
																	 Description = "Welkom boodschap voor huidige gebruiker."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UserNameHeading_", 
																	 Value = "Gebruikersnaam", 
																	 Description = "Gebruikersnaam hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UserNameMessage_", 
																	 Value = "Unieke gebruikersnaam AUB", 
																	 Description = "Gebruikersnaam boodschap voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_PasswordHeading_", 
																	 Value = "Wachtwoord", 
																	 Description = "Wachtwoord hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_RememberMeHeading_", 
																	 Value = "Aangemeld Blijven", 
																	 Description = "Aangemeld blijven hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DashboardGreeting_", 
																	 Value = "Welkom bij het Dashboard", 
																	 Description = "Heading for dashboard screen."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DashboardSplash_", 
																	 Value = "Deze pagina zal het dashboard huisvesten, aanvankelijk zichtbaar voor alle gebruikers die ingelogd zijn.", 
																	 Description = "Content holder for dashboard screen."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AddHeading_", 
																	 Value = "Toevoegen", 
																	 Description = "Toevoegen hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_SearchMessage_", 
																	 Value = "Typ om te zoeken", 
																	 Description = "Zoeken hoofd voor hergebruik"
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_RefreshHeading_", 
																	 Value = "Verversen", 
																	 Description = "Verversen hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_EditHeading_", 
																	 Value = "Redigeren", 
																	 Description = "Redigeren hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_IdentityHeading_", 
																	 Value = "Id", 
																	 Description = "Identiteit hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_RoleHeading_", 
																	 Value = "Rol", 
																	 Description = "Rol hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_ActiveHeading_", 
																	 Value = "Aktief", 
																	 Description = "Aktief hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AddUserHeading_", 
																	 Value = "Gebruiker Toevoegen", 
																	 Description = "Gebruiker toevoegen hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_EditUserHeading_", 
																	 Value = "Gebruiker Redigeren", 
																	 Description = "Gebruiker redigeren hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_SaveHeading_", 
																	 Value = "Opslaan", 
																	 Description = "Opslaan hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UpdateHeading_", 
																	 Value = "Opslaan", 
																	 Description = "Opdateren hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_CancelHeading_", 
																	 Value = "Anuleer", 
																	 Description = "Anuleer hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AllUsersHeading_", 
																	 Value = "Alle Gebruikers", 
																	 Description = "Alle gebruikers hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_ActiveUsersHeading_", 
																	 Value = "Aktieve Gebruikers", 
																	 Description = "Aktieve gebruikers hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_InactiveUsersHeading_", 
																	 Value = "Onaktieve Gebruikers", 
																	 Description = "Onaktieve gebruikers hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_LanguageHeading_", 
																	 Value = "Taal", 
																	 Description = "Taal hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_EnglishHeading_", 
																	 Value = "Engels", 
																	 Description = "Engels hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DutchHeading_", 
																	 Value = "Nederlands", 
																	 Description = "Nederlands hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AuthAdminHeading_", 
																	 Value = "Autorisatie Administratie", 
																	 Description = "AuthAdmin hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_ElementHeading_", 
																	 Value = "Permissie Element", 
																	 Description = "Element hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DomainPermissionHeading_", 
																	 Value = "Domein Permissie", 
																	 Description = "Domein Permissie hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_FunctionalPermissionHeading_", 
																	 Value = "Functionele Permissie", 
																	 Description = "Functionele Permissie hoofd voor hergebruik."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_PermissionHeading_", 
																	 Value = "Permissie", 
																	 Description = "Permissie hoofd voor hergebruik."
				                         }
#endregion
			                         }
		};

		private static LocalizationCollectionEntity LocaleDefaultFile = new LocalizationCollectionEntity()
		{
			Locale = "default",
			LocalizationItems = new List<LocalizationEntity>()
			                         {
#region items
				                         new LocalizationEntity()
				                         {
					                         Key = "_MainTitle_", 
																	 Value = "Singleton Theory Auth.", 
																	 Description = "Main app title."
				                         },
																  new LocalizationEntity()
				                         {
					                          Key = "_LogInHeading_", 
																	 Value = "Log In", 
																	 Description = "Log in heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_HomeAnchor_", 
																	 Value = "Home", 
																	 Description = "Main Nav Bar Home Item"
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AdminActionsAnchor_", 
																	 Value = "Administrator Actions", 
																	 Description = "Main Nav Bar Admin Actions Item"
				                         },
																  new LocalizationEntity()
				                         {
					                          Key = "_AdministrateUsersHeading_", 
																	 Value = "Adminstrate Users", 
																	 Description = "Administrate Users heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_LogOutAnchor_", 
																	 Value = "Log Out", 
																	 Description = "Main Nav Bar Logout Item"
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UserWelcomeMessage_", 
																	 Value = "Welcome", 
																	 Description = "Welcome message for logged in user."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UserNameHeading_", 
																	 Value = "Username", 
																	 Description = "User name heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UserNameMessage_", 
																	 Value = "Enter a unique username", 
																	 Description = "User name message for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_PasswordHeading_", 
																	 Value = "Password", 
																	 Description = "Password heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_RememberMeHeading_", 
																	 Value = "Remember Me", 
																	 Description = "Rememberme heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DashboardGreeting_", 
																	 Value = "Welcome to the Dashboard.", 
																	 Description = "Heading for dashboard screen."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DashboardSplash_", 
																	 Value = "This view will house the dashboard, initially visible to all logged in users.", 
																	 Description = "Content holder for dashboard screen."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AddHeading_", 
																	 Value = "Add", 
																	 Description = "Add heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_SearchMessage_", 
																	 Value = "Start typing to Search", 
																	 Description = "Search message for re-use"
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_RefreshHeading_", 
																	 Value = "Refresh", 
																	 Description = "Refresh heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_EditHeading_", 
																	 Value = "Edit", 
																	 Description = "Edit heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_IdentityHeading_", 
																	 Value = "Id", 
																	 Description = "Identity heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_RoleHeading_", 
																	 Value = "Role", 
																	 Description = "Role heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_ActiveHeading_", 
																	 Value = "Active", 
																	 Description = "Active heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AddUserHeading_", 
																	 Value = "Add User", 
																	 Description = "Add user heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_EditUserHeading_", 
																	 Value = "Edit User", 
																	 Description = "Edit user heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_SaveHeading_", 
																	 Value = "Save", 
																	 Description = "Save heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_UpdateHeading_", 
																	 Value = "Update", 
																	 Description = "Update heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_CancelHeading_", 
																	 Value = "Cancel", 
																	 Description = "Cancel heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AllUsersHeading_", 
																	 Value = "All Users", 
																	 Description = "All users heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_ActiveUsersHeading_", 
																	 Value = "Active Users", 
																	 Description = "Active users heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_InactiveUsersHeading_", 
																	 Value = "In-Active Users", 
																	 Description = "In-Active users heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_LanguageHeading_", 
																	 Value = "Language", 
																	 Description = "Language heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_EnglishHeading_", 
																	 Value = "English", 
																	 Description = "English heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DutchHeading_", 
																	 Value = "Dutch", 
																	 Description = "Dutch heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_AuthAdminHeading_", 
																	 Value = "Authorization Administration", 
																	 Description = "AuthAdmin heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_ElementHeading_", 
																	 Value = "Permission Element", 
																	 Description = "Element heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_DomainPermissionHeading_", 
																	 Value = "Domain Permission", 
																	 Description = "Domain Permission heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_FunctionalPermissionHeading_", 
																	 Value = "Functional Permission", 
																	 Description = "Function Permission heading for re-use."
				                         },
																  new LocalizationEntity()
				                         {
					                         Key = "_PermissionHeading_", 
																	 Value = "Permission", 
																	 Description = "Permission heading for re-use."
				                         }
#endregion
			                         }
		};

		#region Public Methods

		public static void CreateLanguageFiles()
		{
			Container container = EndpointHost.Config.ServiceManager.Container;
			LocalizationRepository repository = container.Resolve<LocalizationRepository>();

			repository.ClearCollection();
			repository.Create(LocaleDefaultFile);
			repository.Create(LocaleUSFile);
			repository.Create(LocaleNLFile);
		}

		#endregion Public Methods
	}
}