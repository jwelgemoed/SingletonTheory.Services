using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;

namespace SingletonTheory.Services.AuthServices.Utilities
{
	public static class PermissionUtility
	{
		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";

		private const string RolesCollection = "Roles";
		private const string FunctionalPermissionsCollection = "FunctionalPermissions";
		private const string DomainPermissionsCollection = "DomainPermissions";
		private const string PermissionsCollection = "Permissions";
		#endregion Constants

		#region Public Methods

		public static List<string> GetFunctionalPermissionNamesForRoleIdsAndDomainPermissions(List<int> roleIds, List<DomainPermissionObject> domainPermissionObjects, string timeZoneId)
		{
			List<string> functionalPersmissionNameList = new List<string>();
			List<int> domainPermissionIds = new List<int>();
			List<int> functionalPermissionIds = new List<int>();
			//Get domainpermission ids for roles
			AddDomainPermissionIdsForRoleIds(roleIds, domainPermissionIds);

			//get active domain permissionids from DomainPermissionObjects
			AddDomainPermissionIdsForDomainPermissionObjects(domainPermissionObjects, domainPermissionIds, timeZoneId);

			//Get all functional permissions ids for domainpermissions
			GetFunctionalPermissionIdsForDomainPermissionIds(domainPermissionIds, functionalPermissionIds);

			// Get all user functional permissions
			GetFunctionalPermissionNames(functionalPermissionIds, functionalPersmissionNameList);

			return functionalPersmissionNameList;
		}

		public static List<string> GetPermissionNamesForRoleIdsAndDomainPermissions(List<int> roleIds, List<DomainPermissionObject> domainPermissionObjects, string timeZoneId)
		{
			List<string> persmissionNameList = new List<string>();
			List<int> domainPermissionIds = new List<int>();
			List<int> functionalPermissionIds = new List<int>();
			List<int> permissionIds = new List<int>();

			//Get domainpermission ids for roles
			AddDomainPermissionIdsForRoleIds(roleIds, domainPermissionIds);

			//get active domain permissionids from DomainPermissionObjects
			AddDomainPermissionIdsForDomainPermissionObjects(domainPermissionObjects, domainPermissionIds, timeZoneId);

			//Get all functional permissions ids for domainpermissions
			GetFunctionalPermissionIdsForDomainPermissionIds(domainPermissionIds, functionalPermissionIds);

			// Get all user permissions ids for functional permission ids
			GetPermissionIdsForFunctionalPermissionIds(functionalPermissionIds, permissionIds);

			// Get all user permissions names
			GetPermissionNames(permissionIds, persmissionNameList);

			return persmissionNameList;
		}

		public static List<int> GetDomainPermissionIdsForRoleId(List<int> roleIds)
		{
			List<int> domainPermissionIds = new List<int>();
			//Get domainpermission ids for roles
			AddDomainPermissionIdsForRoleIds(roleIds, domainPermissionIds);

			return domainPermissionIds;
		}

		#endregion  Public Methods

		#region Private Methods

		private static void GetPermissionNames(List<int> permissionIds, List<string> permissionNameList)
		{
			try
			{
				var permissionEntities = new List<PermissionEntity>();

				permissionEntities = GenericRepository.GetList<PermissionEntity>(AuthAdminDatabase, PermissionsCollection);
				
				if (permissionEntities != null)
				{
					for (int i = 0; i < permissionEntities.Count; i++)
					{
						var permissionEntity = permissionEntities[i];
						if (permissionIds.Contains(permissionEntity.Id))
						{
							if (!permissionNameList.Contains(permissionEntity.Name))
								permissionNameList.Add(permissionEntity.Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private static void GetPermissionIdsForFunctionalPermissionIds(List<int> functionalPermissionIds, List<int> permissionIdList)
		{
			try
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
							if (functionalPermissionEntity.PermissionIds != null && functionalPermissionEntity.PermissionIds.Length > 0)
								permissionIdList.AddRange(functionalPermissionEntity.PermissionIds);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private static void GetFunctionalPermissionNames(List<int> functionalPermissionIds, List<string> functionalPersmissionNameList)
		{
			try
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
							if (!functionalPersmissionNameList.Contains(functionalPermissionEntity.Name))
								functionalPersmissionNameList.Add(functionalPermissionEntity.Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private static void GetFunctionalPermissionIdsForDomainPermissionIds(List<int> domainPermissionIds,
			List<int> functionalPermissionIds)
		{
			try
			{
				var domainPermissionEntities = new List<DomainPermissionEntity>();
				domainPermissionEntities = GenericRepository.GetList<DomainPermissionEntity>(AuthAdminDatabase,
					DomainPermissionsCollection);

				if (domainPermissionEntities != null)
				{
					for (int j = 0; j < domainPermissionEntities.Count; j++)
					{
						var domainPermissionEntity = domainPermissionEntities[j];
						if (domainPermissionIds.Contains(domainPermissionEntity.Id) && domainPermissionEntity.FunctionalPermissionIds != null && domainPermissionEntity.FunctionalPermissionIds.Length > 0)
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
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private static void AddDomainPermissionIdsForDomainPermissionObjects(List<DomainPermissionObject> domainPermissionObjects,
			List<int> domainPermissionIds,string timeZoneId)
		{
			try
			{
				for (int index = 0; index < domainPermissionObjects.Count; index++)
				{
					var domainPermissionObject = domainPermissionObjects[index];
					//TODO: date usage to be defined
					if (DateTimeUtility.ConvertTimeFromUtc(domainPermissionObject.ActiveTimeSpan.StartDate, timeZoneId) >= DateTimeUtility.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneId) &&
							DateTimeUtility.ConvertTimeFromUtc(domainPermissionObject.ActiveTimeSpan.EndDate, timeZoneId) >= DateTimeUtility.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneId))
					{
						if (!domainPermissionIds.Contains(domainPermissionObject.DomainPermissionId))
							domainPermissionIds.Add(domainPermissionObject.DomainPermissionId);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		
		}

		private static void AddDomainPermissionIdsForRoleIds(List<int> roleIds, List<int> domainPermissionIds)
		{
			try
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
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		#endregion  Private Methods
	}
}