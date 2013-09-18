using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class RoleService : Service
	{
		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";

		private const string RolesCollection = "Roles";

		#endregion Constants

		#region Role

		public Role Get(Role request)
		{
			if (request.Id != 0)
			{
				RoleEntity entity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, request.Id);
			
				if (entity == null)
					return null;

				return TranslateToResponse(entity);
			}

			return null;
		}

		public Role Post(Role request)
		{
			RoleEntity entity = TranslateToEntity(request);

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<RoleEntity>(AuthAdminDatabase, RolesCollection);

			entity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, entity);

			return TranslateToResponse(entity);
		}
		
		public Role Put(Role request)
		{
			RoleEntity entity = TranslateToEntity(request);

			entity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, entity);

			return TranslateToResponse(entity);
		}

		public Role Delete(Role request)
		{
			GenericRepository.DeleteById<RoleEntity>(AuthAdminDatabase, RolesCollection, request.Id);

			return null;
		}

		#endregion Role

		#region Roles

		public List<Role> Get(Roles role)
		{
			List<RoleEntity> entities = new List<RoleEntity>();

			entities = GenericRepository.GetList<RoleEntity>(AuthAdminDatabase, RolesCollection);

			if (entities == null)
				return null;

			return TranslateToResponse(entities);
		}

		#endregion Roles

		#region Private Methods

		//private UserRepository GetRepository()
		//{
		//	UserRepository repository = base.GetResolver().TryResolve<UserRepository>();
		//	return repository;
		//}

		private Role TranslateToResponse(RoleEntity entity)
		{
			Role response = entity.TranslateTo<Role>();

			return response;
		}

		private List<Role> TranslateToResponse(List<RoleEntity> entities)
		{
			List<Role> response = new List<Role>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		private static RoleEntity TranslateToEntity(Role request)
		{
			RoleEntity response = request.TranslateTo<RoleEntity>();

			return response;
		}

		#endregion Private Methods
	}
}