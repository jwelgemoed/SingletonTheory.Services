using System;
using System.Collections.Generic;
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
using SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin;
using SingletonTheory.Services.AuthServices.Utilities;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class FunctionalPermissionService : Service
	{
		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";
		private const string FunctionalPermissionsCollection = "FunctionalPermissions";

		#endregion Constants

		#region FunctionalPermission

		public FunctionalPermission Get(FunctionalPermission request)
		{
			if (request.Id != 0)
			{
				FunctionalPermissionEntity entity = GenericRepository.GetItemTopById<FunctionalPermissionEntity>(AuthAdminDatabase, FunctionalPermissionsCollection, request.Id);

				if (entity == null)
					return null;

				return entity.TranslateToResponse();
			}

			return null;
		}

		public FunctionalPermission Post(FunctionalPermission request)
		{
			FunctionalPermissionEntity entity = request.TranslateToEntity();

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<FunctionalPermissionEntity>(AuthAdminDatabase, FunctionalPermissionsCollection);

			entity = GenericRepository.Add(AuthAdminDatabase, FunctionalPermissionsCollection, entity);

			return entity.TranslateToResponse();
		}

		public FunctionalPermission Put(FunctionalPermission request)
		{
			FunctionalPermissionEntity entity = request.TranslateToEntity();

			entity = GenericRepository.Add(AuthAdminDatabase, FunctionalPermissionsCollection, entity);

			return entity.TranslateToResponse();
		}

		public FunctionalPermission Delete(FunctionalPermission request)
		{
			GenericRepository.DeleteById<FunctionalPermissionEntity>(AuthAdminDatabase, FunctionalPermissionsCollection, request.Id);

			return null;
		}

		#endregion FunctionalPermission

		#region FunctionalPermissions

		public List<FunctionalPermission> Get(FunctionalPermissions role)
		{
			var entities = new List<FunctionalPermissionEntity>();

			entities = GenericRepository.GetList<FunctionalPermissionEntity>(AuthAdminDatabase, FunctionalPermissionsCollection);

			if (entities == null)
				return null;

			return entities.TranslateToResponse();
		}

		#endregion FunctionalPermissions

	}
}