using MongoDB.Driver;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class CustomMongoDBAuthRepository : MongoDBAuthRepository, ICustomUserAuthRepository
	{
		private MongoDatabase _mongoDatabase;
		public CustomMongoDBAuthRepository(MongoDatabase mongoDatabase, bool createMissingCollections)
			: base(mongoDatabase, createMissingCollections)
		{
			_mongoDatabase = mongoDatabase;
		}

		public void ClearUserAuths()
		{
			if (_mongoDatabase.CollectionExists("UserAuth"))
				_mongoDatabase.DropCollection("UserAuth");
		}

		public List<UserAuth> GetAllUserAuths()
		{
			var users = _mongoDatabase.GetCollection<UserAuth>("UserAuth");
			MongoCursor<UserAuth> cursor = users.FindAllAs<UserAuth>();
			return cursor.ToList();
		}
	}
}