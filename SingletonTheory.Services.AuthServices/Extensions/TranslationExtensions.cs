using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SingletonTheory.Services.AuthServices.Utilities;

namespace SingletonTheory.Services.AuthServices.Extensions
{
	public static class TranslationExtensions
	{
		#region Role

		public static Role TranslateToResponse(this RoleEntity entity)
		{
			Role response = entity.TranslateTo<Role>();

			return response;
		}

		public static List<Role> TranslateToResponse(this List<RoleEntity> entities)
		{
			List<Role> response = new List<Role>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		public static RoleEntity TranslateToEntity(this Role request)
		{
			RoleEntity response = request.TranslateTo<RoleEntity>();
			
			return response;
		}

		#endregion Role

		#region DomainPermission

		public static DomainPermission TranslateToResponse(this DomainPermissionEntity entity)
		{
			DomainPermission response = entity.TranslateTo<DomainPermission>();

			return response;
		}


		public static List<DomainPermission> TranslateToResponse(this List<DomainPermissionEntity> entities)
		{
			List<DomainPermission> response = new List<DomainPermission>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		public static DomainPermissionEntity TranslateToEntity(this DomainPermission request)
		{
			DomainPermissionEntity response = request.TranslateTo<DomainPermissionEntity>();

			return response;
		}

		#endregion DomainPermission

		#region FunctionalPermission
		
		public static FunctionalPermission TranslateToResponse(this FunctionalPermissionEntity entity)
		{
			FunctionalPermission response = entity.TranslateTo<FunctionalPermission>();

			//language name to label
			response.Label = LocalizationUtility.GetL18nLabel(response.Name);
			return response;
		}

		public static List<FunctionalPermission> TranslateToResponse(this List<FunctionalPermissionEntity> entities)
		{
			List<FunctionalPermission> response = new List<FunctionalPermission>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			LocalizationUtility.ApplyLanguagingToLabels(new List<INameLabel>(response));

			return response;
		}

		public static FunctionalPermissionEntity TranslateToEntity(this FunctionalPermission request)
		{
			FunctionalPermissionEntity response = request.TranslateTo<FunctionalPermissionEntity>();

			return response;
		}

		#endregion FunctionalPermission

		#region Permission

		public static Permission TranslateToResponse(this PermissionEntity entity)
		{
			Permission response = entity.TranslateTo<Permission>();

			//language name to label
			response.Label = LocalizationUtility.GetL18nLabel(response.Name);
			return response;
		}

		public static List<Permission> TranslateToResponse(this List<PermissionEntity> entities)
		{
			List<Permission> response = new List<Permission>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			LocalizationUtility.ApplyLanguagingToLabels(new List<INameLabel>(response));

			return response;
		}

		public static PermissionEntity TranslateToEntity(this Permission request)
		{
			PermissionEntity response = request.TranslateTo<PermissionEntity>();
			
			return response;
		}

		#endregion Permission
	}
}