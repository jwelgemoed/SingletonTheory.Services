using System;
using System.Collections.Generic;
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

				return TranslateToResponse(entity);
			}

			return null;
		}

		public DomainPermission Post(DomainPermission request)
		{
			DomainPermissionEntity entity = TranslateToEntity(request);

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection);

			entity = GenericRepository.Add(AuthAdminDatabase, DomainPermissionsCollection, entity);

			return TranslateToResponse(entity);
		}

		public DomainPermission Put(DomainPermission request)
		{
			DomainPermissionEntity entity = TranslateToEntity(request);

			entity = GenericRepository.Add(AuthAdminDatabase, DomainPermissionsCollection, entity);

			return TranslateToResponse(entity);
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

			return TranslateToResponse(entities);
		}

		#endregion DomainPermissions

		#region Private Methods

		//private UserRepository GetRepository()
		//{
		//	UserRepository repository = base.GetResolver().TryResolve<UserRepository>();
		//	return repository;
		//}

		private DomainPermission TranslateToResponse(DomainPermissionEntity entity)
		{
			DomainPermission response = entity.TranslateTo<DomainPermission>();

			return response;
		}

		private List<DomainPermission> TranslateToResponse(List<DomainPermissionEntity> entities)
		{
			List<DomainPermission> response = new List<DomainPermission>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		private static DomainPermissionEntity TranslateToEntity(DomainPermission request)
		{
			DomainPermissionEntity response = request.TranslateTo<DomainPermissionEntity>();

			return response;
		}

		#endregion Private Methods
	}
}