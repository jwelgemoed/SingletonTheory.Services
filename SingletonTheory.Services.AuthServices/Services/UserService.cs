using MongoDB.Bson;
using ServiceStack.Common;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class UserService : Service
	{
		#region Public Methods

		public User Get(User request)
		{
			UserRepository repository = GetRepository();

			if (!string.IsNullOrEmpty(request.UserName))
			{
				// Get user with UserName
				UserEntity userEntity = repository.Read(request.UserName);

				if (userEntity == null)
					return null;

				return TranslateToResponse(userEntity);
			}
			else if (request.Id != ObjectId.Empty)
			{
				// Get user with UserName
				UserEntity userEntity = repository.Read(request.Id);

				if (userEntity == null)
					return null;

				return TranslateToResponse(userEntity);
			}

			return null;
		}

		public List<User> Get(Users request)
		{
			UserRepository repository = GetRepository();
			List<UserEntity> userEntities = repository.Read();

			return TranslateToResponse(userEntities);
		}

		public List<User> Post(Users request)
		{
			UserRepository repository = GetRepository();
			List<UserEntity> userEntities = null;
			if (request.UserNames.Count != 0)
			{
				userEntities = repository.Read(request.UserNames);
			}
			else if (request.UserNames.Count != 0)
			{
				userEntities = repository.Read(request.UserIds);
			}

			return userEntities == null ? null : TranslateToResponse(userEntities);
		}

		public User Put(User request)
		{
			UserRepository repository = GetRepository();
			UserEntity userEntity = TranslateToEntity(request);

			userEntity = repository.Update(userEntity);

			return TranslateToResponse(userEntity);
		}

		public User Post(User request)
		{
			UserRepository repository = GetRepository();
			UserEntity entity = TranslateToEntity(request);

			entity = repository.Create(entity);

			return TranslateToResponse(entity);
		}

		#endregion Public Methods

		#region Private Methods

		private UserRepository GetRepository()
		{
			UserRepository repository = base.GetResolver().TryResolve<UserRepository>();
			return repository;
		}

		private User TranslateToResponse(UserEntity entity)
		{
			User response = entity.TranslateTo<User>();

			return response;
		}

		private List<User> TranslateToResponse(List<UserEntity> entities)
		{
			List<User> users = new List<User>();
			for (int i = 0; i < entities.Count; i++)
			{
				users.Add(TranslateToResponse(entities[i]));
			}

			return users;
		}

		private static UserEntity TranslateToEntity(User request)
		{
			UserEntity response = request.TranslateTo<UserEntity>();

			response.PasswordHash = request.Password;

			return response;
		}

		#endregion Private Methods
	}
}