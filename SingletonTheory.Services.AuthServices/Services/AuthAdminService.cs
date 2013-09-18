using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SingletonTheory.Services.AuthServices.Services
{
	[Authenticate]
	public class AuthAdminService : Service
	{
		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";

		private const string RolesCollection = "Roles";
		private const string FunctionalPermissionsCollection = "FunctionalPermissions";
		private const string DomainPermissionsCollection = "DomainPermissions";
		private const string PermissionsCollection = "Permissions";

		#endregion Constants

		#region Assigned Unassigned List

		public LevelLists Get(LevelLists request)
		{
			if (request.RoleId != 0)
			{
				//Get The Role
				GetRoleLevelLists(request);
			}
			else if (request.DomainPermissionId != 0)
			{
				//Get The Role
				GetDomainPermissionLists(request);
			}
			else if (request.FunctionalPermissionId != 0)
			{
				//Get The Role
				GetFunctionalPermissionLists(request);
			}
			//	ApplyLanguagingToLabels(new List<INameLabel>(responseList));

			return request;
		}

		#endregion Assigned Unassigned List

		#region Private Methods

		private static void GetRoleLevelLists(LevelLists request)
		{
			Role role = GenericRepository.GetItemTopById<Role>(AuthAdminDatabase, RolesCollection, request.RoleId);
			int[] assigned;

			if (role != null && role.DomainPermissionIds != null)
			{
				assigned = new int[role.DomainPermissionIds.Length];
				//Set assigned roles
				for (int i = 0; i < role.DomainPermissionIds.Length; i++)
				{
					var obj = GenericRepository.GetItemTopById<DomainPermission>(AuthAdminDatabase, DomainPermissionsCollection, role.DomainPermissionIds[i]);
					if (obj == null)
						continue;
					request.Assigned.Add(obj);
					assigned[i] = obj.Id;
				}

				//Set unassigned roles
				var domainPermissions = GenericRepository.GetList<DomainPermission>(AuthAdminDatabase, DomainPermissionsCollection);
				for (int i = 0; i < domainPermissions.Count; i++)
				{
					if (!assigned.Contains(domainPermissions[i].Id))
					{
						request.UnAssigned.Add(domainPermissions[i]);
					}
				}
			}
		}

		private static void GetDomainPermissionLists(LevelLists request)
		{
			DomainPermission domainPermission = GenericRepository.GetItemTopById<DomainPermission>(AuthAdminDatabase, FunctionalPermissionsCollection, request.DomainPermissionId);
			int[] assigned;

			if (domainPermission != null && domainPermission.FunctionalPermissionIds != null)
			{
				assigned = new int[domainPermission.FunctionalPermissionIds.Length];
				//Set assigned roles
				for (int i = 0; i < domainPermission.FunctionalPermissionIds.Length; i++)
				{
					var obj = GenericRepository.GetItemTopById<FunctionalPermission>(AuthAdminDatabase, FunctionalPermissionsCollection, domainPermission.FunctionalPermissionIds[i]);
					if (obj == null)
						continue;
					request.Assigned.Add(obj);
					assigned[i] = obj.Id;
				}

				//Set unassigned roles
				var functionalPermissions = GenericRepository.GetList<FunctionalPermission>(AuthAdminDatabase, FunctionalPermissionsCollection);
				for (int i = 0; i < functionalPermissions.Count; i++)
				{
					if (!assigned.Contains(functionalPermissions[i].Id))
					{
						request.UnAssigned.Add(functionalPermissions[i]);
					}
				}
			}
		}

		private static void GetFunctionalPermissionLists(LevelLists request)
		{
			FunctionalPermission functional = GenericRepository.GetItemTopById<FunctionalPermission>(AuthAdminDatabase, DomainPermissionsCollection, request.FunctionalPermissionId);
			int[] assigned;

			if (functional != null && functional.PermissionIds != null)
			{
				assigned = new int[functional.PermissionIds.Length];
				//Set assigned roles
				for (int i = 0; i < functional.PermissionIds.Length; i++)
				{
					var obj = GenericRepository.GetItemTopById<Permission>(AuthAdminDatabase, PermissionsCollection, functional.PermissionIds[i]);
					if (obj == null)
						continue;
					request.Assigned.Add(obj);
					assigned[i] = obj.Id;
				}

				//Set unassigned roles
				var permisssions = GenericRepository.GetList<Permission>(AuthAdminDatabase, PermissionsCollection);
				for (int i = 0; i < permisssions.Count; i++)
				{
					if (!assigned.Contains(permisssions[i].Id))
					{
						request.UnAssigned.Add(permisssions[i]);
					}
				}
			}
		}

		#endregion Private Methods
	}
}