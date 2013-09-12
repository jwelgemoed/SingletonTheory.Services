﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Services
{
	[Authenticate]
	public class AuthAdminService : Service
	{
		#region Constants
		private const string AuthAdminDatabase = "AuthAdminDatabase";

		private const string RolesCollection = "Roles";
		private const string GroupsLvl2Collection = "GroupsLvl2";
		private const string GroupsLvl1Collection = "GroupsLvl1";
		private const string PermissionsCollection = "Permissions";

		#endregion Constants

		#region Role

		public List<Role> Get(Role role)
		{
			return GenericRepository.GetList<Role>(AuthAdminDatabase, RolesCollection);
		}

		public Role Post(Role role)
		{
			if (role.Id == -1)
				role.Id = GenericRepository.GetMaxId<Role>(AuthAdminDatabase, RolesCollection);

			role = GenericRepository.Add(AuthAdminDatabase, RolesCollection, role);

			return role;
		}

		public Role Put(Role role)
		{
			role = GenericRepository.Add(AuthAdminDatabase, RolesCollection, role);

			return role;
		}

		#endregion Role

		#region GroupLvl2

		public List<GroupLvl2> Get(GroupLvl2 groupLvl2)
		{
			return GenericRepository.GetList<GroupLvl2>(AuthAdminDatabase, GroupsLvl2Collection);
		}

		public GroupLvl2 Post(GroupLvl2 groupLvl2)
		{
			if (groupLvl2.Id == -1)
				groupLvl2.Id = GenericRepository.GetMaxId<GroupLvl2>(AuthAdminDatabase, GroupsLvl2Collection);

			groupLvl2 = GenericRepository.Add(AuthAdminDatabase, GroupsLvl2Collection, groupLvl2);

			return groupLvl2;
		}

		public GroupLvl2 Put(GroupLvl2 groupLvl2)
		{
			groupLvl2 = GenericRepository.Add(AuthAdminDatabase, GroupsLvl2Collection, groupLvl2);

			return groupLvl2;
		}

		#endregion GroupLvl2

		#region GroupLvl1

		public List<GroupLvl1> Get(GroupLvl1 groupLvl1)
		{
			return GenericRepository.GetList<GroupLvl1>(AuthAdminDatabase, GroupsLvl1Collection);
		}

		public GroupLvl1 Post(GroupLvl1 groupLvl1)
		{
			if (groupLvl1.Id == -1)
				groupLvl1.Id = GenericRepository.GetMaxId<GroupLvl1>(AuthAdminDatabase, GroupsLvl1Collection);

			groupLvl1 = GenericRepository.Add(AuthAdminDatabase, GroupsLvl1Collection, groupLvl1);

			return groupLvl1;
		}

		public GroupLvl1 Put(GroupLvl1 groupLvl1)
		{
			groupLvl1 = GenericRepository.Add(AuthAdminDatabase, GroupsLvl1Collection, groupLvl1);

			return groupLvl1;
		}

		#endregion GroupLvl1

		#region Permission

		public List<Permission> Get(Permission permission)
		{
			return GenericRepository.GetList<Permission>(AuthAdminDatabase, PermissionsCollection);
		}

		public Permission Post(Permission permission)
		{
			if (permission.Id == -1)
				permission.Id = GenericRepository.GetMaxId<Permission>(AuthAdminDatabase, PermissionsCollection);

			permission = GenericRepository.Add(AuthAdminDatabase, PermissionsCollection, permission);

			return permission;
		}

		public Permission Put(Permission permission)
		{
			permission = GenericRepository.Add(AuthAdminDatabase, PermissionsCollection, permission);

			return permission;
		}

		public Permission Delete(Permission permission)
		{
			GenericRepository.DeleteById<Permission>(AuthAdminDatabase, PermissionsCollection, permission.Id.ToString(CultureInfo.InvariantCulture));

			return null;
		}

		#endregion Permission
	}
}