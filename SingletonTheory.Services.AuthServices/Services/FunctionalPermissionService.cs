using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
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

				return TranslateToResponse(entity);
			}

			return null;
		}

		public FunctionalPermission Post(FunctionalPermission request)
		{
			FunctionalPermissionEntity entity = TranslateToEntity(request);

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<FunctionalPermissionEntity>(AuthAdminDatabase, FunctionalPermissionsCollection);

			entity = GenericRepository.Add(AuthAdminDatabase, FunctionalPermissionsCollection, entity);

			return TranslateToResponse(entity);
		}

		public FunctionalPermission Put(FunctionalPermission request)
		{
			FunctionalPermissionEntity entity = TranslateToEntity(request);

			entity = GenericRepository.Add(AuthAdminDatabase, FunctionalPermissionsCollection, entity);

			return TranslateToResponse(entity);
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

			return TranslateToResponse(entities);
		}

		#endregion FunctionalPermissions

		#region Private Methods

		//private UserRepository GetRepository()
		//{
		//	UserRepository repository = base.GetResolver().TryResolve<UserRepository>();
		//	return repository;
		//}

		private FunctionalPermission TranslateToResponse(FunctionalPermissionEntity entity)
		{
			FunctionalPermission response = entity.TranslateTo<FunctionalPermission>();

			//language name to label
			response.Label = LocalizationUtility.GetL18nLabel(response.Name);
			return response;
		}

		private List<FunctionalPermission> TranslateToResponse(List<FunctionalPermissionEntity> entities)
		{
			List<FunctionalPermission> response = new List<FunctionalPermission>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			LocalizationUtility.ApplyLanguagingToLabels(new List<INameLabel>(response));

			return response;
		}

		private static FunctionalPermissionEntity TranslateToEntity(FunctionalPermission request)
		{
			FunctionalPermissionEntity response = request.TranslateTo<FunctionalPermissionEntity>();

			return response;
		}

		#endregion Private Methods
	}
}