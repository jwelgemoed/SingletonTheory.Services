﻿using MongoDB.Driver;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Logging.Log4Net;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Admin;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Data;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Data;
using SingletonTheory.Services.AuthServices.Data.Hours;
using SingletonTheory.Services.AuthServices.Providers;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.Repositories.Hours;
using SingletonTheory.Services.AuthServices.Services;
using SingletonTheory.Services.AuthServices.Validations;
using System;
using System.Collections.Generic;
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
			//AddPlugins(Plugins);
			//RegisterContainerItems(container);
			//RegisterValidations(container);
			//RegisterLogProvider();

			//// TODO:  Remove this and replace with permanent solutions.
			//CreateMockData(container);
			Configure(container, Plugins);
			ServiceExceptionHandler = new HandleServiceExceptionDelegate(AppHost_ExceptionHandler);
		}

		public static void Configure(Funq.Container container, List<IPlugin> plugins)
		{
			AddPlugins(plugins);
			RegisterContainerItems(container);
			RegisterValidations(container);
			RegisterLogProvider();

			// TODO:  Remove this and replace with permanent solutions.
			CreateMockData(container);
		}

		/// <summary>
		/// Handles all exceptions that happens in Services.
		/// </summary>
		/// <param name="request">The request object that caused the exception.</param>
		/// <param name="ex">The exception that happened</param>
		/// <returns>Default handling of exceptions as by DtoUtils.HandleException</returns>
		private object AppHost_ExceptionHandler(object request, Exception ex)
		{
			LogManager.LogFactory.GetLogger(this.GetType()).Error(ex.Message, ex);

			return DtoUtils.HandleException(this, request, ex);
		}

		#endregion Override Methods

		#region Static Methods

		private static void RegisterLogProvider()
		{
			LogManager.LogFactory = new Log4NetFactory(true);
		}

		private static void RegisterContainerItems(Funq.Container container)
		{
			container.Register<ICacheClient>(new MemoryCacheClient());
			container.Register<SSAuthInterfaces.IUserAuthRepository>(GetUserAuthRepositoryProvider());
			container.Register<LocalizationRepository>(GetLocalizationRepositoryProvider());
			container.Register<UserRepository>(GetUserRepositoryProvider());

			//Types
			container.Register<TitleRepository>(GetTitleRepositoryProvider());
			container.Register<ContactTypeRepository>(GetContactTypeRepositoryProvider());
			container.Register<EntityTypeRepository>(GetEntityTypeRepositoryProvider());
			container.Register<OccupationNameRepository>(GetOccupationNameRepositoryProvider());
			container.Register<GenderTypeRepository>(GetGenderTypeRepositoryProvider());
			container.Register<AddressTypeRepository>(GetAddressTypeRepositoryProvider());
			container.Register<HourTypeRepository>(GetHourTypeRepositoryProvider());
			container.Register<CostCentreRepository>(GetCostCentreRepositoryProvider());


			container.Register<ContactRepository>(GetContactRepositoryProvider());
			container.Register<AddressRepository>(GetAddressRepositoryProvider());
			container.Register<EntityRelationshipRepository>(GetEntityRelationshipRepositoryProvider());
			container.Register<EntityRepository>(GetEntityRepositoryProvider());
			container.Register<PersonRepository>(GetPersonRepositoryProvider());
			container.Register<ItemHoursRepository>(GetItemHoursRepositoryProvider());
			container.Register<RoomHoursRepository>(GetRoomHoursRepositoryProvider());
			container.Register<EmployeeRepository>(GetEmployeeRepositoryProvider());

		}

		public static void CreateMockData(Funq.Container container)
		{
			LocalizationRepository repository = container.Resolve<LocalizationRepository>();
			if (repository == null)
				throw new InvalidOperationException("LocalizationRepository not defined in IoC Container");

			LocalizationData.CreateLanguageFiles(repository, ConfigSettings.LocalizationFilePath);

			PermissionData.CreatePermissions(ConfigSettings.PermissionsDirectory);

			UserData.CreateUsers();
			ContactDetailsData.CreateData();
			HoursData.CreateData();
		}

		private static void RegisterValidations(Funq.Container container)
		{
			container.RegisterValidators(typeof(UserRequestValidator).Assembly);
		}

		private static UserRepository GetUserRepositoryProvider()
		{
			return new UserRepository(ConfigSettings.UserDatabaseConnectionName);
		}

		private static ContactRepository GetContactRepositoryProvider()
		{
			return new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static AddressRepository GetAddressRepositoryProvider()
		{
			return new AddressRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static EmployeeRepository GetEmployeeRepositoryProvider()
		{
			return new EmployeeRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static EntityRelationshipRepository GetEntityRelationshipRepositoryProvider()
		{
			return new EntityRelationshipRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static EntityRepository GetEntityRepositoryProvider()
		{
			return new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static PersonRepository GetPersonRepositoryProvider()
		{
			return new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}
		
		private static GenderTypeRepository GetGenderTypeRepositoryProvider()
		{
			return new GenderTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static AddressTypeRepository GetAddressTypeRepositoryProvider()
		{
			return new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static TitleRepository GetTitleRepositoryProvider()
		{
			return new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static ContactTypeRepository GetContactTypeRepositoryProvider()
		{
			return new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static EntityTypeRepository GetEntityTypeRepositoryProvider()
		{
			return new EntityTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static OccupationNameRepository GetOccupationNameRepositoryProvider()
		{
			return new OccupationNameRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		private static ItemHoursRepository GetItemHoursRepositoryProvider()
		{
			return new ItemHoursRepository(ConfigSettings.HoursDatabaseConnectionName);
		}

		private static RoomHoursRepository GetRoomHoursRepositoryProvider()
		{
			return new RoomHoursRepository(ConfigSettings.HoursDatabaseConnectionName);
		}

		private static CostCentreRepository GetCostCentreRepositoryProvider()
		{
			return new CostCentreRepository(ConfigSettings.HoursDatabaseConnectionName);
		}

		private static HourTypeRepository GetHourTypeRepositoryProvider()
		{
			return new HourTypeRepository(ConfigSettings.HoursDatabaseConnectionName);
		}

		private static SSAuthInterfaces.IUserAuthRepository GetUserAuthRepositoryProvider()
		{
			MongoDatabase database = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, ConfigSettings.MongoUserAuthDatabaseName);

			return new UserAuthRepository(database, true);
		}

		private static LocalizationRepository GetLocalizationRepositoryProvider()
		{
			MongoDatabase database = MongoWrapper.GetDatabase(ConfigSettings.MongoConnectionString, ConfigSettings.MongoLocalizationDatabaseName);

			return new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
		}

		#endregion Static Methods

		#region Private Methods

		public static void AddPlugins(List<IPlugin> plugins)
		{
			AppSettings appSettings = new AppSettings();
			SSAuthInterfaces.AuthUserSession authUserSession = new SSAuthInterfaces.AuthUserSession();
			AuthProvider authProvider = new AuthProvider();
			SSAuthInterfaces.IAuthProvider[] authProviders = new SSAuthInterfaces.IAuthProvider[] { authProvider };
			RequestLogsFeature requestLogsFeature = new RequestLogsFeature();

			requestLogsFeature.RequiredRoles = new string[] { };

			plugins.Add(new AuthFeature(() => authUserSession, authProviders) { });
			plugins.Add(requestLogsFeature);
			plugins.Add(new RegistrationFeature());
			plugins.Add(new ValidationFeature());
		}

		#endregion Private Methods
	}
}