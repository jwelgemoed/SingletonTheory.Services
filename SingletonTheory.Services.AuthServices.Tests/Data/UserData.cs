using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class UserData
	{
		public static UserEntity GetUserForInsert()
		{
			string hash;
			string salt;
			new SaltedHash().GetHashAndSaltString(HTTPClientHelpers.Password, out hash, out salt);
			UserEntity entity = new UserEntity()
			{
				Language = "",
				ModifiedDate = DateTime.UtcNow,
				PasswordHash = hash,
				Salt = salt,
				UserName = HTTPClientHelpers.AdminUserName
			};

			entity.Active = true;
			entity.Language = "en-US";
			entity.Permissions.Add("SomePermission");
			entity.Roles.Add(1);

			return entity;
		}

		internal static List<UserEntity> GetUsersForInsert()
		{
			List<UserEntity> entities = new List<UserEntity>();
			entities.Add(GetUserForInsert());
			UserEntity entity = GetUserForInsert();
			entity.UserName = "SomeOtherUser";
			entities.Add(entity);

			return entities;
		}
	}
}
