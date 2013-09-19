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
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class DomainPermissionService : Service
	{
		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";
		private const string DomainPermissionsCollection = "DomainPermissions";

		#endregion Constants

		#region DomainPermission

		public DomainPermission Get(DomainPermission request)
		{
			if (request.Id != 0)
			{
				DomainPermissionEntity entity = GenericRepository.GetItemTopById<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection, request.Id);

				if (entity == null)
					return null;

				return entity.TranslateToResponse();
			}

			return null;
		}

		public DomainPermission Post(DomainPermission request)
		{
			DomainPermissionEntity entity = request.TranslateToEntity();

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection);

			entity = GenericRepository.Add(AuthAdminDatabase, DomainPermissionsCollection, entity);

			return entity.TranslateToResponse();
		}

		public DomainPermission Put(DomainPermission request)
		{
			DomainPermissionEntity entity = request.TranslateToEntity();

			entity = GenericRepository.Add(AuthAdminDatabase, DomainPermissionsCollection, entity);

			return entity.TranslateToResponse();
		}

		public DomainPermission Delete(DomainPermission request)
		{
			GenericRepository.DeleteById<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection, request.Id);

			return null;
		}

		#endregion DomainPermission

		#region DomainPermissions

		public List<DomainPermission> Get(DomainPermissions role)
		{
			List<DomainPermissionEntity> entities = new List<DomainPermissionEntity>();

			entities = GenericRepository.GetList<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection);

			if (entities == null)
				return null;

			return entities.TranslateToResponse();
		}

		#endregion DomainPermissions

	}
}