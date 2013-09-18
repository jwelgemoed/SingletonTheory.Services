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

				return TranslateToResponse(entity);
			}

			return null;
		}

		public Permission Post(Permission request)
		{
			PermissionEntity entity = TranslateToEntity(request);

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<PermissionEntity>(AuthAdminDatabase, PermissionsCollection);

			entity = GenericRepository.Add(AuthAdminDatabase, PermissionsCollection, entity);

			return TranslateToResponse(entity);
		}

		public Permission Put(Permission request)
		{
			PermissionEntity entity = TranslateToEntity(request);

			entity = GenericRepository.Add(AuthAdminDatabase, PermissionsCollection, entity);

			return TranslateToResponse(entity);
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

			return TranslateToResponse(entities);
		}

		#endregion Permissions

		#region Private Methods

		//private UserRepository GetRepository()
		//{
		//	UserRepository repository = base.GetResolver().TryResolve<UserRepository>();
		//	return repository;
		//}

		private Permission TranslateToResponse(PermissionEntity entity)
		{
			Permission response = entity.TranslateTo<Permission>();

			//language name to label
			response.Label = LocalizationUtility.GetL18nLabel(response.Name);
			return response;
		}

		private List<Permission> TranslateToResponse(List<PermissionEntity> entities)
		{
			List<Permission> response = new List<Permission>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			LocalizationUtility.ApplyLanguagingToLabels(new List<INameLabel>(response));

			return response;
		}

		private static PermissionEntity TranslateToEntity(Permission request)
		{
			PermissionEntity response = request.TranslateTo<PermissionEntity>();

			return response;
		}

		#endregion Private Methods
	}
}