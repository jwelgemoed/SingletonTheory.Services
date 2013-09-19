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
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SingletonTheory.Services.AuthServices.Utilities;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class PermissionService : Service
	{
		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";
		private const string PermissionsCollection = "Permissions";

		#endregion Constants

		#region Permission

		public Permission Get(Permission request)
		{
			if (request.Id != 0)
			{
				PermissionEntity entity = GenericRepository.GetItemTopById<PermissionEntity>(AuthAdminDatabase, PermissionsCollection, request.Id);

				if (entity == null)
					return null;

				return entity.TranslateToResponse();
			}

			return null;
		}

		public Permission Post(Permission request)
		{
			PermissionEntity entity = request.TranslateToEntity();

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<PermissionEntity>(AuthAdminDatabase, PermissionsCollection);

			entity = GenericRepository.Add(AuthAdminDatabase, PermissionsCollection, entity);

			return entity.TranslateToResponse();
		}

		public Permission Put(Permission request)
		{
			PermissionEntity entity = request.TranslateToEntity();

			entity = GenericRepository.Add(AuthAdminDatabase, PermissionsCollection, entity);

			return entity.TranslateToResponse();
		}

		public Permission Delete(Permission request)
		{
			GenericRepository.DeleteById<RoleEntity>(AuthAdminDatabase, PermissionsCollection, request.Id);

			return null;
		}

		#endregion Permission

		#region Permissions

		public List<Permission> Get(Permissions role)
		{
			var entities = new List<PermissionEntity>();

			entities = GenericRepository.GetList<PermissionEntity>(AuthAdminDatabase, PermissionsCollection);

			if (entities == null)
				return null;

			return entities.TranslateToResponse();
		}

		#endregion Permissions
	}
}