using MongoDB.Driver;
using ServiceStack.ServiceInterface.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingletonTheory.Services.AuthServices.Interfaces;

namespace SingletonTheory.Services.AuthServices.Repositories
{
    public class CustomMongoDBAuthRepository :MongoDBAuthRepository, ICustomUserAuthRepository
    {
        private MongoDatabase _mongoDatabase;
        public CustomMongoDBAuthRepository(MongoDatabase mongoDatabase, bool createMissingCollections)
            : base(mongoDatabase, createMissingCollections)
        {
            _mongoDatabase = mongoDatabase;
        }

        public List<UserAuth> GetAllUserAuths()
        {
            var users = _mongoDatabase.GetCollection<UserAuth>("UserAuth");
            MongoCursor<UserAuth> cursor = users.FindAllAs<UserAuth>();
            return cursor.ToList();
        }
    }
}