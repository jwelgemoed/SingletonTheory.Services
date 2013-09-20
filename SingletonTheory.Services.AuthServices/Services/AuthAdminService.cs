using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Extensions;
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

		#region Get Put

		public LevelLists Get(LevelLists request)
		{
			if (request.RoleId != 0)
			{
				GetRoleLevelLists(request);
			}
			else if (request.DomainPermissionId != 0)
			{
				GetDomainPermissionLists(request);
			}
			else if (request.FunctionalPermissionId != 0)
			{
				GetFunctionalPermissionLists(request);
			}
			//	ApplyLanguagingToLabels(new List<INameLabel>(responseList));

			return request;
		}

		public LevelLists Put(LevelLists request)
		{
			if (request.RoleId != 0)
			{
				SetRoleLevelLists(request);
				GetRoleLevelLists(request);
			}
			else if (request.DomainPermissionId != 0)
			{
				SetDomainPermissionLists(request);
				GetDomainPermissionLists(request);
			}
			else if (request.FunctionalPermissionId != 0)
			{
				SetFunctionalPermissionLists(request);
				GetFunctionalPermissionLists(request);
			}
			//	ApplyLanguagingToLabels(new List<INameLabel>(responseList));

			return request;
		}

		#endregion Get Put

		#region Role

		private static void GetRoleLevelLists(LevelLists request)
		{
			RoleEntity roleEntity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, request.RoleId);
			int[] assigned = new int[] {};

			if (roleEntity != null)
			{
				if (roleEntity.DomainPermissionIds != null)
				{
					assigned = new int[roleEntity.DomainPermissionIds.Length];
					//Set assigned roles
					for (int i = 0; i < roleEntity.DomainPermissionIds.Length; i++)
					{
						var obj = GenericRepository.GetItemTopById<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection,
							roleEntity.DomainPermissionIds[i]);
						if (obj == null)
							continue;
						request.Assigned.Add(obj.TranslateToResponse());
						assigned[i] = obj.Id;
					}
				}

				//Set unassigned roles
				var domainPermissions = GenericRepository.GetList<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection);
				for (int i = 0; i < domainPermissions.Count; i++)
				{
					if (roleEntity.DomainPermissionIds == null || !assigned.Contains(domainPermissions[i].Id))
					{
						request.UnAssigned.Add(domainPermissions[i].TranslateToResponse());
					}
				}
			}
		}

		private static void SetRoleLevelLists(LevelLists request)
		{
			RoleEntity entity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, request.RoleId);
			int[] assigned;

			if (entity != null)
			{
				assigned = new int[request.Assigned.Count];

				for (int i = 0; i < request.Assigned.Count; i++)
				{
					var obj = (DomainPermission)request.Assigned[i];
					assigned[i] = obj.Id;
				}
				entity.DomainPermissionIds = assigned;
				GenericRepository.Add(AuthAdminDatabase, RolesCollection, entity);
			}
		}

		#endregion Role

		#region DomainPermission

		private static void GetDomainPermissionLists(LevelLists request)
		{
			DomainPermissionEntity domainPermissionEntity = GenericRepository.GetItemTopById<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection, request.DomainPermissionId);
			int[] assigned = new int[] { };

			if (domainPermissionEntity != null)
			{
				if (domainPermissionEntity.FunctionalPermissionIds != null)
				{
					assigned = new int[domainPermissionEntity.FunctionalPermissionIds.Length];
					//Set assigned
					for (int i = 0; i < domainPermissionEntity.FunctionalPermissionIds.Length; i++)
					{
						var obj = GenericRepository.GetItemTopById<FunctionalPermissionEntity>(AuthAdminDatabase,
							FunctionalPermissionsCollection, domainPermissionEntity.FunctionalPermissionIds[i]);
						if (obj == null)
							continue;
						request.Assigned.Add(obj.TranslateToResponse());
						assigned[i] = obj.Id;
					}
				}

				//Set unassigned
				var functionalPermissions = GenericRepository.GetList<FunctionalPermissionEntity>(AuthAdminDatabase, FunctionalPermissionsCollection);
				for (int i = 0; i < functionalPermissions.Count; i++)
				{
					if (domainPermissionEntity.FunctionalPermissionIds == null || !assigned.Contains(functionalPermissions[i].Id))
					{
						request.UnAssigned.Add(functionalPermissions[i].TranslateToResponse());
					}
				}
			}
		}

		private static void SetDomainPermissionLists(LevelLists request)
		{
			DomainPermissionEntity entity = GenericRepository.GetItemTopById<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection, request.DomainPermissionId);
			int[] assigned;

			if (entity != null)
			{
				assigned = new int[request.Assigned.Count];

				for (int i = 0; i < request.Assigned.Count; i++)
				{
					var obj = (FunctionalPermission)request.Assigned[i];
					assigned[i] = obj.Id;
				}
				entity.FunctionalPermissionIds = assigned;
				GenericRepository.Add(AuthAdminDatabase, DomainPermissionsCollection, entity);
			}
		}

		#endregion DomainPermission

		#region FunctionalPermission

		private static void GetFunctionalPermissionLists(LevelLists request)
		{
			FunctionalPermissionEntity functionalPermissionEntity = GenericRepository.GetItemTopById<FunctionalPermissionEntity>(AuthAdminDatabase, FunctionalPermissionsCollection, request.FunctionalPermissionId);
			int[] assigned = new int[] {};

			if (functionalPermissionEntity != null)
			{
				if (functionalPermissionEntity.PermissionIds != null)
				{
					assigned = new int[functionalPermissionEntity.PermissionIds.Length];
					//Set assigned roles
					for (int i = 0; i < functionalPermissionEntity.PermissionIds.Length; i++)
					{
						var obj = GenericRepository.GetItemTopById<PermissionEntity>(AuthAdminDatabase, PermissionsCollection,
							functionalPermissionEntity.PermissionIds[i]);
						if (obj == null)
							continue;
						request.Assigned.Add(obj.TranslateToResponse());
						assigned[i] = obj.Id;
					}
				}

				//Set unassigned roles
				var permisssions = GenericRepository.GetList<PermissionEntity>(AuthAdminDatabase, PermissionsCollection);
				for (int i = 0; i < permisssions.Count; i++)
				{
					if (functionalPermissionEntity.PermissionIds == null || !assigned.Contains(permisssions[i].Id))
					{
						request.UnAssigned.Add(permisssions[i].TranslateToResponse());
					}
				}
			}
		}

		private static void SetFunctionalPermissionLists(LevelLists request)
		{
			FunctionalPermissionEntity entity = GenericRepository.GetItemTopById<FunctionalPermissionEntity>(AuthAdminDatabase, FunctionalPermissionsCollection, request.FunctionalPermissionId);
			int[] assigned;

			if (entity != null)
			{
				assigned = new int[request.Assigned.Count];

				for (int i = 0; i < request.Assigned.Count; i++)
				{
					var obj = (Permission)request.Assigned[i];
					assigned[i] = obj.Id;
				}
				entity.PermissionIds = assigned;
				GenericRepository.Add(AuthAdminDatabase, FunctionalPermissionsCollection, entity);
			}
		}

		#endregion FunctionalPermission
	}
}