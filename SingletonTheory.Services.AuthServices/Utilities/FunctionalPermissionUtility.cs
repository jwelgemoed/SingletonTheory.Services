using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;

namespace SingletonTheory.Services.AuthServices.Utilities
{
	public static class FunctionalPermissionUtility
	{
		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";

		private const string RolesCollection = "Roles";
		private const string FunctionalPermissionsCollection = "FunctionalPermissions";
		private const string DomainPermissionsCollection = "DomainPermissions";
		
		#endregion Constants

		public static List<string> GetFunctionalPermissionNamesForRoleIdsAndDomainPermissions(List<int> roleIds, List<DomainPermissionObject> domainPermissionObjects)
		{
			List<string> functionalPersmissionNameList = new List<string>();
			List<int> domainPermissionIds = new List<int>();
			List<int> functionalPermissionIds = new List<int>();
			//Get domainpermission ids for roles
			AddDomainPermissionIdsForRoleIds(roleIds, domainPermissionIds);

			//get active domain permissionids from DomainPermissionObjects
			AddDomainPermissionIdsForDomainPermissionObjects(domainPermissionObjects, domainPermissionIds);

			//Get all functional permissions ids for domainpermissions
			GetFunctionalPermissionIdsForDomainPermissionIds(domainPermissionIds, functionalPermissionIds);

			// Get all user functional permissions
			GetFunctionalPermissionNames(functionalPermissionIds, functionalPersmissionNameList);

			return functionalPersmissionNameList;
		}

		private static void GetFunctionalPermissionNames(List<int> functionalPermissionIds, List<string> functionalPersmissionNameList)
		{
			var functionalPermissionEntities = new List<FunctionalPermissionEntity>();

			functionalPermissionEntities = GenericRepository.GetList<FunctionalPermissionEntity>(AuthAdminDatabase,
				FunctionalPermissionsCollection);

			if (functionalPermissionEntities != null)
			{
				for (int i = 0; i < functionalPermissionEntities.Count; i++)
				{
					var functionalPermissionEntity = functionalPermissionEntities[i];
					if (functionalPermissionIds.Contains(functionalPermissionEntity.Id))
					{
						functionalPersmissionNameList.Add(functionalPermissionEntity.Name);
					}
				}
			}
		}

		private static void GetFunctionalPermissionIdsForDomainPermissionIds(List<int> domainPermissionIds,
			List<int> functionalPermissionIds)
		{
			var domainPermissionEntities = new List<DomainPermissionEntity>();
			domainPermissionEntities = GenericRepository.GetList<DomainPermissionEntity>(AuthAdminDatabase,
				DomainPermissionsCollection);

			if (domainPermissionEntities != null)
			{
				for (int j = 0; j < domainPermissionEntities.Count; j++)
				{
					var domainPermissionEntity = domainPermissionEntities[j];
					if (domainPermissionIds.Contains(domainPermissionEntity.Id) &&
					    domainPermissionEntity.FunctionalPermissionIds.Length > 0)
					{
						for (int i = 0; i < domainPermissionEntity.FunctionalPermissionIds.Length; i++)
						{
							var fId = domainPermissionEntity.FunctionalPermissionIds[i];
							if (!functionalPermissionIds.Contains(fId))
								functionalPermissionIds.Add(fId);
						}
					}
				}
			}
		}

		private static void AddDomainPermissionIdsForDomainPermissionObjects(List<DomainPermissionObject> domainPermissionObjects,
			List<int> domainPermissionIds)
		{
			for (int index = 0; index < domainPermissionObjects.Count; index++)
			{
				var domainPermissionObject = domainPermissionObjects[index];
//TODO: date usage to be defined
				if (domainPermissionObject.ActiveTimeSpan.StartDate >= DateTime.UtcNow &&
				    domainPermissionObject.ActiveTimeSpan.EndDate >= DateTime.UtcNow)
				{
					if (!domainPermissionIds.Contains(domainPermissionObject.DomainPermissionId))
						domainPermissionIds.Add(domainPermissionObject.DomainPermissionId);
				}
			}
		}

		private static void AddDomainPermissionIdsForRoleIds(List<int> roleIds, List<int> domainPermissionIds)
		{
			for (int i = 0; i < roleIds.Count; i++)
			{
				RoleEntity entity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, roleIds[i]);

				if (entity != null)
				{
					if (entity.DomainPermissionIds != null && entity.DomainPermissionIds.Length > 0)
					{
						domainPermissionIds.AddRange(entity.DomainPermissionIds);
					}
				}
			}
		}
	}
}