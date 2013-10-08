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
using SingletonTheory.Services.AuthServices.Extensions;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin;

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

				return entity.TranslateToResponse();
			}

			return null;
		}

		public Role Post(Role request)
		{
			RoleEntity entity = request.TranslateToEntity();

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<RoleEntity>(AuthAdminDatabase, RolesCollection);

			entity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, entity);

			return entity.TranslateToResponse();
		}
		
		public Role Put(Role request)
		{
			RoleEntity entity = request.TranslateToEntity();

			entity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, entity);

			return entity.TranslateToResponse();
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

			return entities.TranslateToResponse();
		}

		#endregion Roles
	}
}