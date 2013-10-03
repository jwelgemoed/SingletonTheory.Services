using MongoDB.Driver;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Admin;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Data;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Data;
using SingletonTheory.Services.AuthServices.Providers;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Services;
using SingletonTheory.Services.AuthServices.Validations;
using System;
using SSAuthInterfaces = ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Host
{
	public class AppHost : AppHostBase
	{
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
			RegisterContainerItems(container);
			RegisterValidations(container);
			RegisterLogProvider();

			// TODO:  Remove this and replace with permanent solutions.
			CreateMockData(container);
		}

		#endregion Override Methods

		#region Static Methods

		private static void RegisterLogProvider()
		{
			//LogManager.LogFactory = new Log4NetFactory(true);
		}

		private static void RegisterContainerItems(Funq.Container container)
		{
			container.Register<ICacheClient>(new MemoryCacheClient());
			container.Register<SSAuthInterfaces.IUserAuthRepository>(GetUserAuthRepositoryProvider());
			container.Register<LocalizationRepository>(GetLocalizationRepositoryProvider());
			container.Register<UserRepository>(GetUserRepositoryProvider());
		}

		private static void CreateMockData(Funq.Container container)
		{
			LocalizationRepository repository = container.Resolve<LocalizationRepository>();
			if (repository == null)
				throw new InvalidOperationException("LocalizationRepository not defined in IoC Container");

			LocalizationData.CreateLanguageFiles(repository, ConfigSettings.LocalizationFilePath);

			UserData.CreateUsers();
		}

		private static void RegisterValidations(Funq.Container container)
		{
			container.RegisterValidators(typeof(UserRequestValidator).Assembly);
		}

		private static UserRepository GetUserRepositoryProvider()
		{
			MongoDatabase database = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, ConfigSettings.MongoUserDatabaseName);

			return new UserRepository(database);
		}

		private static SSAuthInterfaces.IUserAuthRepository GetUserAuthRepositoryProvider()
		{
			MongoDatabase database = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, ConfigSettings.MongoUserAuthDatabaseName);

			return new UserAuthRepository(database, true);
		}

		private static LocalizationRepository GetLocalizationRepositoryProvider()
		{
			MongoDatabase database = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, ConfigSettings.MongoLocalizationDatabaseName);

			return new LocalizationRepository(database);
		}

		#endregion Static Methods

		#region Private Methods

		private void AddPlugins()
		{
			AppSettings appSettings = new AppSettings();
			SSAuthInterfaces.AuthUserSession authUserSession = new SSAuthInterfaces.AuthUserSession();
			AuthProvider authProvider = new AuthProvider();
			SSAuthInterfaces.IAuthProvider[] authProviders = new SSAuthInterfaces.IAuthProvider[] { authProvider };
			RequestLogsFeature requestLogsFeature = new RequestLogsFeature();

			requestLogsFeature.RequiredRoles = new string[] { };

			Plugins.Add(new AuthFeature(() => authUserSession, authProviders) { });
			Plugins.Add(requestLogsFeature);
			Plugins.Add(new RegistrationFeature());
			Plugins.Add(new ValidationFeature());
		}

		#endregion Private Methods
	}
}