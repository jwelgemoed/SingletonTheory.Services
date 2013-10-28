using Funq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.Common;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SingletonTheory.Services.AuthServices.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using SSAuthInterfaces = ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class UserAuthRepository : SSAuthInterfaces.IUserAuthRepository, SSAuthInterfaces.IClearable
	{
		#region Fields & Properties

		//http://stackoverflow.com/questions/3588623/c-sharp-regex-for-a-username-with-a-few-restrictions
		public Regex ValidUserNameRegEx = new Regex(@"^(?=.{3,15}$)([A-Za-z0-9][._-]?)*$", RegexOptions.Compiled);

		private readonly MongoDatabase _mongoDatabase;

		// UserOAuthProvider collection name
		private static string UserOAuthProvider_Col
		{
			get
			{
				return typeof(SSAuthInterfaces.UserOAuthProvider).Name;
			}
		}
		// Counters collection name
		private static string Counters_Col
		{
			get
			{
				return typeof(Counters).Name;
			}
		}

		#endregion Fields & Properties

		#region Constructors

		public UserAuthRepository(MongoDatabase mongoDatabase, bool createMissingCollections)
		{
			this._mongoDatabase = mongoDatabase;

			if (createMissingCollections)
			{
				CreateMissingCollections();
			}

			if (!CollectionsExists())
			{
				throw new InvalidOperationException("One of the collections needed by MongoDBAuthRepository is missing." +
													"You can call MongoDBAuthRepository constructor with the parameter CreateMissingCollections set to 'true'  " +
													"to create the needed collections.");
			}
		}

		#endregion Constructors

		#region Public Methods

		private void ValidateNewUser(SSAuthInterfaces.UserAuth user, string password)
		{
			user.ThrowIfNull("newUser");
			password.ThrowIfNullOrEmpty("password");

			if (user.UserName.IsNullOrEmpty() && user.Email.IsNullOrEmpty())
				throw new ArgumentNullException("UserName or Email is required");

			if (!user.UserName.IsNullOrEmpty())
			{
				if (!ValidUserNameRegEx.IsMatch(user.UserName))
					throw new ArgumentException("UserName contains invalid characters", "UserName");
			}
		}

		public SSAuthInterfaces.UserAuth CreateUserAuth(SSAuthInterfaces.UserAuth user, string password)
		{
			throw new NotImplementedException();
		}

		public SSAuthInterfaces.UserAuth GetUserAuth(string userAuthId)
		{
			//var collection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserAuth>(UserAuth_Col);
			//SSAuthInterfaces.UserAuth userAuth = collection.FindOneById(int.Parse(userAuthId));
			//return userAuth;

			return null;
		}

		public void SaveUserAuth(SSAuthInterfaces.IAuthSession authSession)
		{
			//var userAuth = !authSession.UserAuthId.IsNullOrEmpty()
			//	? GetUserAuth(authSession.UserAuthId)
			//	: authSession.TranslateTo<SSAuthInterfaces.UserAuth>();

			//if (userAuth.Id == default(int) && !authSession.UserAuthId.IsNullOrEmpty())
			//	userAuth.Id = int.Parse(authSession.UserAuthId);

			//userAuth.ModifiedDate = DateTime.UtcNow;
			//if (userAuth.CreatedDate == default(DateTime))
			//	userAuth.CreatedDate = userAuth.ModifiedDate;

			//var collection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserAuth>(UserAuth_Col);
			//SaveUser(userAuth);

			throw new NotImplementedException();
		}

		public void SaveUserAuth(SSAuthInterfaces.UserAuth userAuth)
		{
			userAuth.ModifiedDate = DateTime.UtcNow;
			if (userAuth.CreatedDate == default(DateTime))
				userAuth.CreatedDate = userAuth.ModifiedDate;

			SaveUser(userAuth);
		}

		public List<SSAuthInterfaces.UserOAuthProvider> GetUserOAuthProviders(string userAuthId)
		{
			var id = int.Parse(userAuthId);

			IMongoQuery query = Query.EQ("UserAuthId", int.Parse(userAuthId));

			var collection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserOAuthProvider>(UserOAuthProvider_Col);
			MongoCursor<SSAuthInterfaces.UserOAuthProvider> queryResult = collection.Find(query);

			return queryResult.ToList();
		}

		public SSAuthInterfaces.UserAuth GetUserAuth(SSAuthInterfaces.IAuthSession authSession, SSAuthInterfaces.IOAuthTokens tokens)
		{
			//if (!authSession.UserAuthId.IsNullOrEmpty())
			//{
			//	var userAuth = GetUserAuth(authSession.UserName);
			//	if (userAuth != null) return userAuth;
			//}

			if (!authSession.UserName.IsNullOrEmpty())
			{
				var userAuth = GetUserAuthByUserName(authSession.UserName);
				if (userAuth != null) return userAuth;
			}

			if (!authSession.UserAuthName.IsNullOrEmpty())
			{
				var userAuth = GetUserAuthByUserName(authSession.UserAuthName);
				if (userAuth != null) return userAuth;
			}

			if (tokens == null || tokens.Provider.IsNullOrEmpty() || tokens.UserId.IsNullOrEmpty())
				return null;

			var query = Query.And(
							Query.EQ("Provider", tokens.Provider),
							Query.EQ("UserId", tokens.UserId)
						);

			var providerCollection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserOAuthProvider>(UserOAuthProvider_Col);
			var oAuthProvider = providerCollection.FindOne(query);

			if (oAuthProvider != null)
			{
				//var userAuthCollection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserAuth>(UserAuth_Col);
				//var userAuth = userAuthCollection.FindOneById(oAuthProvider.UserAuthId);
				//return userAuth;

				return GetUserAuthByUserName(oAuthProvider.UserName);
			}

			return null;
		}

		public string CreateOrMergeAuthSession(SSAuthInterfaces.IAuthSession authSession, SSAuthInterfaces.IOAuthTokens tokens)
		{
			var userAuth = GetUserAuth(authSession, tokens) ?? new SSAuthInterfaces.UserAuth();

			var query = Query.And(
							Query.EQ("Provider", tokens.Provider),
							Query.EQ("UserId", tokens.UserId)
						);
			var providerCollection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserOAuthProvider>(UserOAuthProvider_Col);
			var oAuthProvider = providerCollection.FindOne(query);

			if (oAuthProvider == null)
			{
				oAuthProvider = new SSAuthInterfaces.UserOAuthProvider
				{
					Provider = tokens.Provider,
					UserId = tokens.UserId,
				};
			}

			oAuthProvider.PopulateMissing(tokens);
			userAuth.PopulateMissing(oAuthProvider);

			userAuth.ModifiedDate = DateTime.UtcNow;
			if (userAuth.CreatedDate == default(DateTime))
				userAuth.CreatedDate = userAuth.ModifiedDate;

			SaveUser(userAuth);

			if (oAuthProvider.Id == default(int))
				oAuthProvider.Id = IncUserOAuthProviderCounter();

			oAuthProvider.UserAuthId = userAuth.Id;

			if (oAuthProvider.CreatedDate == default(DateTime))
				oAuthProvider.CreatedDate = userAuth.ModifiedDate;
			oAuthProvider.ModifiedDate = userAuth.ModifiedDate;

			providerCollection.Save(oAuthProvider);

			return oAuthProvider.UserAuthId.ToString(CultureInfo.InvariantCulture);
		}

		public void Clear()
		{
			DropAndReCreateCollections();
		}

		public SSAuthInterfaces.UserAuth GetUserAuthByUserName(string userNameOrEmail)
		{
			return GetUserAuthByUserName(_mongoDatabase, userNameOrEmail);
		}

		public SSAuthInterfaces.UserAuth GetUserAuthByUserName(string userNameOrEmail, UserRepository userRepository)
		{
			return GetUserAuthByUserName(_mongoDatabase, userNameOrEmail, userRepository);
		}

		public bool TryAuthenticate(string userName, string password, out SSAuthInterfaces.UserAuth userAuth)
		{
			//userId = null;
			userAuth = GetUserAuthByUserName(userName);
			if (userAuth == null) return false;

			var saltedHash = new SSAuthInterfaces.SaltedHash();
			if (saltedHash.VerifyHashString(password, userAuth.PasswordHash, userAuth.Salt))
			{
				//userId = userAuth.Id.ToString(CultureInfo.InvariantCulture);
				return true;
			}

			userAuth = null;
			return false;
		}

		public bool TryAuthenticate(Dictionary<string, string> digestHeaders, string PrivateKey, int NonceTimeOut, string sequence, out SSAuthInterfaces.UserAuth userAuth)
		{
			//userId = null;
			userAuth = GetUserAuthByUserName(digestHeaders["username"]);
			if (userAuth == null) return false;

			var digestHelper = new SSAuthInterfaces.DigestAuthFunctions();
			if (digestHelper.ValidateResponse(digestHeaders, PrivateKey, NonceTimeOut, userAuth.DigestHA1Hash, sequence))
			{
				//userId = userAuth.Id.ToString(CultureInfo.InvariantCulture);
				return true;
			}
			userAuth = null;
			return false;
		}

		public void LoadUserAuth(SSAuthInterfaces.IAuthSession session, SSAuthInterfaces.IOAuthTokens tokens)
		{
			session.ThrowIfNull("session");

			var userAuth = GetUserAuth(session, tokens);
			LoadUserAuth(session, userAuth);
		}

		public SSAuthInterfaces.UserAuth UpdateUserAuth(SSAuthInterfaces.UserAuth existingUser, SSAuthInterfaces.UserAuth newUser, string password)
		{
			ValidateNewUser(newUser, password);

			AssertNoExistingUser(_mongoDatabase, newUser, existingUser);

			var hash = existingUser.PasswordHash;
			var salt = existingUser.Salt;
			if (password != null)
			{
				var saltedHash = new SSAuthInterfaces.SaltedHash();
				saltedHash.GetHashAndSaltString(password, out hash, out salt);
			}

			// If either one changes the digest hash has to be recalculated
			var digestHash = existingUser.DigestHA1Hash;
			if (password != null || existingUser.UserName != newUser.UserName)
			{
				var digestHelper = new SSAuthInterfaces.DigestAuthFunctions();
				digestHash = digestHelper.CreateHa1(newUser.UserName, SSAuthInterfaces.DigestAuthProvider.Realm, password);
			}

			newUser.Id = existingUser.Id;
			newUser.PasswordHash = hash;
			newUser.Salt = salt;
			newUser.DigestHA1Hash = digestHash;
			newUser.CreatedDate = existingUser.CreatedDate;
			newUser.ModifiedDate = DateTime.UtcNow;
			SaveUser(newUser);

			return newUser;
		}

		#endregion Public Methods

		#region Private Methods

		private void SaveUser(SSAuthInterfaces.UserAuth userAuth)
		{
			//if (userAuth.Id == default(int))
			//	userAuth.Id = IncUserAuthCounter();
			//var usersCollection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserAuth>(UserAuth_Col);
			//usersCollection.Save(userAuth);
		}

		//private int IncUserAuthCounter()
		//{
		//	return IncCounter("UserAuthCounter").UserAuthCounter;
		//}

		private int IncUserOAuthProviderCounter()
		{
			return IncCounter("UserOAuthProviderCounter").UserOAuthProviderCounter;
		}

		private Counters IncCounter(string counterName)
		{
			var CountersCollection = _mongoDatabase.GetCollection<Counters>(Counters_Col);
			var incId = Update.Inc(counterName, 1);
			var query = Query.Null;
			FindAndModifyResult counterIncResult = CountersCollection.FindAndModify(query, SortBy.Null, incId, true);
			Counters updatedCounters = counterIncResult.GetModifiedDocumentAs<Counters>();
			return updatedCounters;
		}

		private static void AssertNoExistingUser(MongoDatabase mongoDatabase, SSAuthInterfaces.UserAuth newUser, SSAuthInterfaces.UserAuth exceptForExistingUser = null)
		{
			if (newUser.UserName != null)
			{
				var existingUser = GetUserAuthByUserName(mongoDatabase, newUser.UserName);
				if (existingUser != null
					&& (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
					throw new ArgumentException("User {0} already exists".Fmt(newUser.UserName));
			}

			if (newUser.Email != null)
			{
				var existingUser = GetUserAuthByUserName(mongoDatabase, newUser.Email);
				if (existingUser != null
					&& (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
					throw new ArgumentException("Email {0} already exists".Fmt(newUser.Email));
			}
		}

		private static SSAuthInterfaces.UserAuth GetUserAuthByUserName(MongoDatabase mongoDatabase, string userNameOrEmail, UserRepository userRepository = null)
		{
			if (userRepository == null)
			{
				// TODO:  Inject UserRepository from Top Level
				Container container = EndpointHost.Config.ServiceManager.Container;
				userRepository = container.Resolve<UserRepository>();
			}

			UserEntity userEntity = userRepository.Read(userNameOrEmail);

			SSAuthInterfaces.UserAuth userAuth = TranslateToUserAuth(userEntity);

			if (userAuth != null)
			{
				userAuth.Permissions = PermissionUtility.GetPermissionNamesForRoleIdsAndDomainPermissions(userEntity.Roles,
					userEntity.DomainPermissions, userEntity.TimeZoneId);
				//userAuth.Permissions = new List<string>();
				//userAuth.Permissions.Add("DomainPermission_Get");
				//userAuth.Permissions.Add("DomainPermission_Put");
				//userAuth.Permissions.Add("DomainPermission_Post");
				//userAuth.Permissions.Add("DomainPermission_Delete");
				//userAuth.Permissions.Add("DomainPermissions_Get");
				//userAuth.Permissions.Add("FunctionalPermission_Get");
				//userAuth.Permissions.Add("FunctionalPermission_Put");
				//userAuth.Permissions.Add("FunctionalPermission_Post");
				//userAuth.Permissions.Add("FunctionalPermission_Delete");
				//userAuth.Permissions.Add("CurrentUserAuthRequest_Get");
				//userAuth.Permissions.Add("LevelLists_Get");
				//userAuth.Permissions.Add("LevelLists_Put");
				//userAuth.Permissions.Add("LocalizationDictionaryRequest_Get");
				//userAuth.Permissions.Add("LocalizationDictionaryRequest_Put");
				//userAuth.Permissions.Add("LocalizationDictionaryRequest_Post");
				//userAuth.Permissions.Add("Permission_Get");
				//userAuth.Permissions.Add("Permission_Put");
				//userAuth.Permissions.Add("Permission_Post");
				//userAuth.Permissions.Add("Permissions_Get");
				//userAuth.Permissions.Add("Role_Get");
				//userAuth.Permissions.Add("Role_Put");
				//userAuth.Permissions.Add("Role_Post");
				//userAuth.Permissions.Add("Role_Delete");
				//userAuth.Permissions.Add("Roles_Get");
				//userAuth.Permissions.Add("User_Get");
				//userAuth.Permissions.Add("User_Put");
				//userAuth.Permissions.Add("User_Post");
				//userAuth.Permissions.Add("Users_Get");
				//userAuth.Permissions.Add("Users_Post");
			}

			return userAuth;
		}

		public SSAuthInterfaces.UserAuth GetUserAuthByUserNameWithFunctionalPermissions(string userName)
		{
			return GetUserAuthByUserNameWithFunctionalPermissions(_mongoDatabase, userName);
		}

		private static SSAuthInterfaces.UserAuth GetUserAuthByUserNameWithFunctionalPermissions(MongoDatabase mongoDatabase, string userNameOrEmail, UserRepository userRepository = null)
		{
			if (userRepository == null)
			{
				// TODO:  Inject UserRepository from Top Level
				Container container = EndpointHost.Config.ServiceManager.Container;
				userRepository = container.Resolve<UserRepository>();
			}

			UserEntity userEntity = userRepository.Read(userNameOrEmail);

			if(!userEntity.Active)
					throw new AccessViolationException("User not Active!");

			SSAuthInterfaces.UserAuth userAuth = TranslateToUserAuth(userEntity);

			if (userAuth != null)
			{
				userAuth.Permissions = PermissionUtility.GetFunctionalPermissionNamesForRoleIdsAndDomainPermissions(userEntity.Roles,
				userEntity.DomainPermissions, userEntity.TimeZoneId);
			}

			return userAuth;
		}

		public void DropAndReCreateCollections()
		{
			//if (_mongoDatabase.CollectionExists(CollectionName))
			//	_mongoDatabase.DropCollection(CollectionName);

			if (_mongoDatabase.CollectionExists(UserOAuthProvider_Col))
				_mongoDatabase.DropCollection(UserOAuthProvider_Col);

			if (_mongoDatabase.CollectionExists(typeof(Counters).Name))
				_mongoDatabase.DropCollection(typeof(Counters).Name);

			CreateMissingCollections();
		}

		public void CreateMissingCollections()
		{
			//if (!_mongoDatabase.CollectionExists(CollectionName))
			//	_mongoDatabase.CreateCollection(CollectionName);

			if (!_mongoDatabase.CollectionExists(UserOAuthProvider_Col))
				_mongoDatabase.CreateCollection(UserOAuthProvider_Col);

			if (!_mongoDatabase.CollectionExists(typeof(Counters).Name))
			{
				_mongoDatabase.CreateCollection(typeof(Counters).Name);

				var countersCollection = _mongoDatabase.GetCollection<Counters>(typeof(Counters).Name);
				Counters counters = new Counters();
				countersCollection.Save(counters);
			}
		}

		public bool CollectionsExists()
		{
			//return (_mongoDatabase.CollectionExists(CollectionName))
			//	&& (_mongoDatabase.CollectionExists(UserOAuthProvider_Col))
			//		&& (_mongoDatabase.CollectionExists(typeof(Counters).Name));
			return (_mongoDatabase.CollectionExists(typeof(Counters).Name));
		}

		private static SSAuthInterfaces.UserAuth TranslateToUserAuth(UserEntity userEntity)
		{
			SSAuthInterfaces.UserAuth userAuth = userEntity.TranslateTo<SSAuthInterfaces.UserAuth>();

			return userAuth;
		}

		private void LoadUserAuth(SSAuthInterfaces.IAuthSession session, SSAuthInterfaces.UserAuth userAuth)
		{
			if (userAuth == null) return;

			session.PopulateWith(userAuth);
			session.UserAuthId = userAuth.Id.ToString(CultureInfo.InvariantCulture);
			session.ProviderOAuthAccess = GetUserOAuthProviders(session.UserAuthId)
				.ConvertAll(x => (SSAuthInterfaces.IOAuthTokens)x);
		}

		#endregion Private Methods

		#region Internal Classes

		// http://www.mongodb.org/display/DOCS/How+to+Make+an+Auto+Incrementing+Field
		class Counters
		{
			public int Id { get; set; }
			public int UserAuthCounter { get; set; }
			public int UserOAuthProviderCounter { get; set; }
		}

		#endregion Internal Classes

	}
}
