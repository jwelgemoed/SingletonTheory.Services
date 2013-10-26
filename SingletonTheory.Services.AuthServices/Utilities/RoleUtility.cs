using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceClient.Web;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Extensions;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Services;
using SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin;

namespace SingletonTheory.Services.AuthServices.Utilities
{
	public class RoleUtility
	{
		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";

		private const string RolesCollection = "Roles";

		#endregion Constants

		#region Methods

		public static void GetRoleAndSubRoleIds(UserEntity userEntity, List<int> subRoleIds)
		{
			List<Role> subRoles = new List<Role>();
			GetRoleAndSubRoles(userEntity, subRoles);

			subRoleIds.AddRange(subRoles.Select(subRole => subRole.Id));
		}

		public static void GetRoleAndSubRoles(UserEntity userEntity, List<Role> subRoles)
		{
			//Get the rootparent entity
			RoleEntity entity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection,
				userEntity.Roles[0]);

			subRoles.Add(entity.TranslateToResponse());

			AddSubRoles(subRoles, entity);
		}

		private static void AddSubRoles(List<Role> availableRoles, RoleEntity entity)
		{
			if (entity.ChildRoleIds != null)
			{
				foreach (var roleId in entity.ChildRoleIds)
				{
					RoleEntity roleEntity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, roleId);

					if (roleEntity != null && roleEntity.DateTimeDeleted == DateTime.MinValue)
					{
						availableRoles.Add(roleEntity.TranslateToResponse());

						AddSubRoles(availableRoles, roleEntity);
					}
				}
			}
		}

		#endregion Methods
	}
}