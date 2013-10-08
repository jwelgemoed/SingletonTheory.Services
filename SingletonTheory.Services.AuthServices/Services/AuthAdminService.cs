using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Extensions;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System.Linq;
using SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin;

namespace SingletonTheory.Services.AuthServices.Services
{
	[Authenticate]
	public class AuthAdminService : Service
	{
		#region Get Put

		public RoleDomainPermissions Get(RoleDomainPermissions request)
		{
			GetRoleLevelLists(request);

			return request;
		}

		public RoleDomainPermissions Put(RoleDomainPermissions request)
		{
				SetRoleLevelLists(request);
				GetRoleLevelLists(request);
			
			return request;
		}

		public DomainPermissionFunctionalPermissions Get(DomainPermissionFunctionalPermissions request)
		{
			GetDomainPermissionLists(request);

			return request;
		}

		public DomainPermissionFunctionalPermissions Put(DomainPermissionFunctionalPermissions request)
		{
			SetDomainPermissionLists(request);
			GetDomainPermissionLists(request);

			return request;
		}

		public FunctionalPermissionPermissions Get(FunctionalPermissionPermissions request)
		{
			GetFunctionalPermissionLists(request);

			return request;
		}

		public FunctionalPermissionPermissions Put(FunctionalPermissionPermissions request)
		{
			SetFunctionalPermissionLists(request);
			GetFunctionalPermissionLists(request);

			return request;
		}

		#endregion Get Put

		#region Role

		private static void GetRoleLevelLists(RoleDomainPermissions request)
		{
			RoleEntity roleEntity = GenericRepository.GetItemTopById<RoleEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.RolesCollection, request.RoleId);
			int[] assigned = new int[] { };
			request.Assigned.Clear();
			request.UnAssigned.Clear();
			if (roleEntity != null)
			{
				if (roleEntity.DomainPermissionIds != null)
				{
					assigned = new int[roleEntity.DomainPermissionIds.Length];
					//Set assigned roles
					for (int i = 0; i < roleEntity.DomainPermissionIds.Length; i++)
					{
						var obj = GenericRepository.GetItemTopById<DomainPermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.DomainPermissionsCollection,
							roleEntity.DomainPermissionIds[i]);
						if (obj == null)
							continue;
						request.Assigned.Add(obj.TranslateToResponse());
						assigned[i] = obj.Id;
					}
				}

				//Set unassigned roles
				var domainPermissions = GenericRepository.GetList<DomainPermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.DomainPermissionsCollection);
				for (int i = 0; i < domainPermissions.Count; i++)
				{
					if (roleEntity.DomainPermissionIds == null || !assigned.Contains(domainPermissions[i].Id))
					{
						request.UnAssigned.Add(domainPermissions[i].TranslateToResponse());
					}
				}
			}
		}

		private static void SetRoleLevelLists(RoleDomainPermissions request)
		{
			RoleEntity entity = GenericRepository.GetItemTopById<RoleEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.RolesCollection, request.RoleId);
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
				GenericRepository.Add(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.RolesCollection, entity);
			}
		}

		#endregion Role

		#region DomainPermission

		private static void GetDomainPermissionLists(DomainPermissionFunctionalPermissions request)
		{
			DomainPermissionEntity domainPermissionEntity = GenericRepository.GetItemTopById<DomainPermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.DomainPermissionsCollection, request.DomainPermissionId);
			int[] assigned = new int[] { };
			request.Assigned.Clear();
			request.UnAssigned.Clear();

			if (domainPermissionEntity != null)
			{
				if (domainPermissionEntity.FunctionalPermissionIds != null)
				{
					assigned = new int[domainPermissionEntity.FunctionalPermissionIds.Length];
					//Set assigned
					for (int i = 0; i < domainPermissionEntity.FunctionalPermissionIds.Length; i++)
					{
						var obj = GenericRepository.GetItemTopById<FunctionalPermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.FunctionalPermissionsCollection, domainPermissionEntity.FunctionalPermissionIds[i]);
						if (obj == null)
							continue;
						request.Assigned.Add(obj.TranslateToResponse());
						assigned[i] = obj.Id;
					}
				}

				//Set unassigned
				var functionalPermissions = GenericRepository.GetList<FunctionalPermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.FunctionalPermissionsCollection);
				for (int i = 0; i < functionalPermissions.Count; i++)
				{
					if (domainPermissionEntity.FunctionalPermissionIds == null || !assigned.Contains(functionalPermissions[i].Id))
					{
						request.UnAssigned.Add(functionalPermissions[i].TranslateToResponse());
					}
				}
			}
		}

		private static void SetDomainPermissionLists(DomainPermissionFunctionalPermissions request)
		{
			DomainPermissionEntity entity = GenericRepository.GetItemTopById<DomainPermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.DomainPermissionsCollection, request.DomainPermissionId);
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
				GenericRepository.Add(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.DomainPermissionsCollection, entity);
			}
		}

		#endregion DomainPermission

		#region FunctionalPermission

		private static void GetFunctionalPermissionLists(FunctionalPermissionPermissions request)
		{
			FunctionalPermissionEntity functionalPermissionEntity = GenericRepository.GetItemTopById<FunctionalPermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.FunctionalPermissionsCollection, request.FunctionalPermissionId);
			int[] assigned = new int[] { };
			request.Assigned.Clear();
			request.UnAssigned.Clear();

			if (functionalPermissionEntity != null)
			{
				if (functionalPermissionEntity.PermissionIds != null)
				{
					assigned = new int[functionalPermissionEntity.PermissionIds.Length];
					//Set assigned roles
					for (int i = 0; i < functionalPermissionEntity.PermissionIds.Length; i++)
					{
						var obj = GenericRepository.GetItemTopById<PermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.PermissionsCollection,
							functionalPermissionEntity.PermissionIds[i]);
						if (obj == null)
							continue;
						request.Assigned.Add(obj.TranslateToResponse());
						assigned[i] = obj.Id;
					}
				}

				//Set unassigned roles
				var permisssions = GenericRepository.GetList<PermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.PermissionsCollection);
				for (int i = 0; i < permisssions.Count; i++)
				{
					if (functionalPermissionEntity.PermissionIds == null || !assigned.Contains(permisssions[i].Id))
					{
						request.UnAssigned.Add(permisssions[i].TranslateToResponse());
					}
				}
			}
		}

		private static void SetFunctionalPermissionLists(FunctionalPermissionPermissions request)
		{
			FunctionalPermissionEntity entity = GenericRepository.GetItemTopById<FunctionalPermissionEntity>(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.FunctionalPermissionsCollection, request.FunctionalPermissionId);
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
				GenericRepository.Add(ConfigSettings.MongoAuthAdminDatabaseName, GenericRepository.FunctionalPermissionsCollection, entity);
			}
		}

		#endregion FunctionalPermission
	}
}