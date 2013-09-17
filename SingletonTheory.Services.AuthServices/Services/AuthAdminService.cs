using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

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

		#region Fields

		private JsonServiceClient _client;

		#endregion Fields

		#region Constructor

		public AuthAdminService()
		{
			_client = new JsonServiceClient(ConfigSettings.ServiceRootUrl)
			{
				UserName = ConfigSettings.ServiceUserName,
				Password = ConfigSettings.ServicePassword
			};
		}

		#endregion Constructor

		#region Role

		public List<Role> Get(Role role)
		{
			List<Role> responsList = new List<Role>();
			if (role.Id != 0)
			{
				responsList = GenericRepository.GetItemById<Role>(AuthAdminDatabase, RolesCollection, role.Id);
			}
			else
			{
				responsList = GenericRepository.GetList<Role>(AuthAdminDatabase, RolesCollection);
			}
			return responsList;
		}

		public Role Post(Role role)
		{
			if (role.Id == 0)
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
			var responseList = new List<GroupLvl2>();
			if (groupLvl2.Id != 0)
			{
				responseList = GenericRepository.GetItemById<GroupLvl2>(AuthAdminDatabase, GroupsLvl2Collection, groupLvl2.Id);
			}
			else
			{
				responseList = GenericRepository.GetList<GroupLvl2>(AuthAdminDatabase, GroupsLvl2Collection);
			}
			return responseList;
		}

		public GroupLvl2 Post(GroupLvl2 groupLvl2)
		{
			if (groupLvl2.Id == 0)
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
			var responseList = new List<GroupLvl1>();
			if (groupLvl1.Id != 0)
			{
				responseList = GenericRepository.GetItemById<GroupLvl1>(AuthAdminDatabase, GroupsLvl1Collection, groupLvl1.Id);
			}
			else
			{
				responseList = GenericRepository.GetList<GroupLvl1>(AuthAdminDatabase, GroupsLvl1Collection);
			}

			ApplyLanguagingToLabels(new List<INameLabel>(responseList));

			return responseList;
		}

		public GroupLvl1 Post(GroupLvl1 groupLvl1)
		{
			if (groupLvl1.Id == 0)
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
			var responseList = new List<Permission>();
			if (permission.Id != 0)
			{
				responseList = GenericRepository.GetItemById<Permission>(AuthAdminDatabase, PermissionsCollection, permission.Id);
			}
			else
			{
				responseList = GenericRepository.GetList<Permission>(AuthAdminDatabase, PermissionsCollection);
			}

			ApplyLanguagingToLabels(new List<INameLabel>(responseList));

			return responseList;
		}

		public Permission Post(Permission permission)
		{
			if (permission.Id == 0)
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

		#region Private Methods

		private void ApplyLanguagingToLabels(List<INameLabel> responseList)
		{
			//Add l18n here
			AuthService authService = new AuthService();
			var language = "default";
			try
			{
				var currentUser = authService.Get(new CurrentUserAuthRequest());
				language = currentUser.Language;
			}
			catch (Exception) { }

			LocalizationDictionaryRequest request = new LocalizationDictionaryRequest();
			request.Locale = language;
			LocalizationDictionaryResponse localizationList = _client.Get<LocalizationDictionaryResponse>(request);

			foreach (var obj in responseList)
			{
				obj.Label = GetLabelFromLocalizationList(localizationList, "_" + obj.Name + "_");
			}
		}

		private string GetLabelFromLocalizationList(LocalizationDictionaryResponse localizationList, string name)
		{
			foreach (var obj in localizationList.LocalizationItems)
			{
				if (obj.Key.Equals(name))
				{
					return obj.Value;
				}
			}
			return name;
		}

		#endregion Private Methods

	}
}