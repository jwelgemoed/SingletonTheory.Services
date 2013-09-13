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

		public List<User> Get(User request)
		{
			UserRepository repository = GetRepository();
			List<User> response;

			if (!string.IsNullOrEmpty(request.UserName))
			{
				// Get user with UserName
				UserEntity userEntity = repository.Read(request.UserName);

				if (userEntity == null)
					return null;

				response = TranslateToResponse(userEntity);
			}
			//else if (request.Id != 0)
			//{
			//	// Get user with Id
			//	SSAuthInterfaces.UserAuth userAuth = repository.Read(request.Id);

			//	response.Add(userAuth);
			//}
			else
			{
				// Get all users
				List<UserEntity> userEntities = repository.Read();
				response = TranslateToResponse(userEntities);
			}

			return response;
		}

		public List<User> Put(User request)
		{
			UserRepository repository = GetRepository();
			UserEntity userEntity = TranslateToEntity(request);

			userEntity = repository.Update(userEntity);

			List<User> response = TranslateToResponse(userEntity);

			return response;
		}

		public List<User> Post(User request)
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

		private List<User> TranslateToResponse(UserEntity entity)
		{
			User response = entity.TranslateTo<User>();

			return new List<User>() { response };
		}

		private List<User> TranslateToResponse(List<UserEntity> entities)
		{
			List<User> users = new List<User>();
			for (int i = 0; i < entities.Count; i++)
			{
				users.AddRange(TranslateToResponse(entities[i]));
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