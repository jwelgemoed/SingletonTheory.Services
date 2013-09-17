using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.Common;
using ServiceStack.Text;
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

		// UserAuth collection name
		private static string UserAuth_Col
		{
			get
			{
				return typeof(SSAuthInterfaces.UserAuth).Name;
			}
		}
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

		public bool CollectionsExists()
		{
			return (_mongoDatabase.CollectionExists(UserAuth_Col))
					&& (_mongoDatabase.CollectionExists(UserOAuthProvider_Col))
					&& (_mongoDatabase.CollectionExists(Counters_Col));
		}

		public void CreateMissingCollections()
		{
			if (!_mongoDatabase.CollectionExists(UserAuth_Col))
				_mongoDatabase.CreateCollection(UserAuth_Col);

			if (!_mongoDatabase.CollectionExists(UserOAuthProvider_Col))
				_mongoDatabase.CreateCollection(UserOAuthProvider_Col);

			if (!_mongoDatabase.CollectionExists(Counters_Col))
			{
				_mongoDatabase.CreateCollection(Counters_Col);

				var countersCollection = _mongoDatabase.GetCollection<Counters>(Counters_Col);
				Counters counters = new Counters();
				countersCollection.Save(counters);
			}
		}

		public void DropAndReCreateCollections()
		{
			if (_mongoDatabase.CollectionExists(UserAuth_Col))
				_mongoDatabase.DropCollection(UserAuth_Col);

			if (_mongoDatabase.CollectionExists(UserOAuthProvider_Col))
				_mongoDatabase.DropCollection(UserOAuthProvider_Col);

			if (_mongoDatabase.CollectionExists(Counters_Col))
				_mongoDatabase.DropCollection(Counters_Col);

			CreateMissingCollections();
		}

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
			ValidateNewUser(user, password);
			AssertNoExistingUser(_mongoDatabase, user);

			var saltedHash = new SSAuthInterfaces.SaltedHash();
			string salt;
			string hash;
			saltedHash.GetHashAndSaltString(password, out hash, out salt);
			var digestHelper = new SSAuthInterfaces.DigestAuthFunctions();
			user.DigestHA1Hash = digestHelper.CreateHa1(user.UserName, SSAuthInterfaces.DigestAuthProvider.Realm, password);
			user.PasswordHash = hash;
			user.Salt = salt;
			user.CreatedDate = DateTime.UtcNow;
			user.ModifiedDate = user.CreatedDate;

			SaveUser(user);
			return user;
		}

		public SSAuthInterfaces.UserAuth GetUserAuth(string userAuthId)
		{
			var collection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserAuth>(UserAuth_Col);
			SSAuthInterfaces.UserAuth userAuth = collection.FindOneById(int.Parse(userAuthId));
			return userAuth;
		}

		public void SaveUserAuth(SSAuthInterfaces.IAuthSession authSession)
		{
			var userAuth = !authSession.UserAuthId.IsNullOrEmpty()
				? GetUserAuth(authSession.UserAuthId)
				: authSession.TranslateTo<SSAuthInterfaces.UserAuth>();

			if (userAuth.Id == default(int) && !authSession.UserAuthId.IsNullOrEmpty())
				userAuth.Id = int.Parse(authSession.UserAuthId);

			userAuth.ModifiedDate = DateTime.UtcNow;
			if (userAuth.CreatedDate == default(DateTime))
				userAuth.CreatedDate = userAuth.ModifiedDate;

			var collection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserAuth>(UserAuth_Col);
			SaveUser(userAuth);
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
			if (!authSession.UserAuthId.IsNullOrEmpty())
			{
				var userAuth = GetUserAuth(authSession.UserAuthId);
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
				var userAuthCollection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserAuth>(UserAuth_Col);
				var userAuth = userAuthCollection.FindOneById(oAuthProvider.UserAuthId);
				return userAuth;
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
			if (userAuth.Id == default(int))
				userAuth.Id = IncUserAuthCounter();
			var usersCollection = _mongoDatabase.GetCollection<SSAuthInterfaces.UserAuth>(UserAuth_Col);
			usersCollection.Save(userAuth);
		}

		private int IncUserAuthCounter()
		{
			return IncCounter("UserAuthCounter").UserAuthCounter;
		}

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

		private static SSAuthInterfaces.UserAuth GetUserAuthByUserName(MongoDatabase mongoDatabase, string userNameOrEmail)
		{
			var isEmail = userNameOrEmail.Contains("@");
			var collection = mongoDatabase.GetCollection<SSAuthInterfaces.UserAuth>(UserAuth_Col);

			IMongoQuery query = isEmail
				? Query.EQ("Email", userNameOrEmail)
				: Query.EQ("UserName", userNameOrEmail);

			SSAuthInterfaces.UserAuth userAuth = collection.FindOne(query);
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
