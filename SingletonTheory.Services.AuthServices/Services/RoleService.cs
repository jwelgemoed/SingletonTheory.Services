using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Extensions;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class RoleService : Service
	{
		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";

		private const string RolesCollection = "Roles";

		#endregion Constants

		#region Role

		public Role Get(Role request)
		{
			if (request.Id != 0)
			{
				RoleEntity entity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, request.Id);

				if (entity == null)
					return null;

				return entity.TranslateToResponse();
			}

			return null;
		}

		public Role Post(Role request)
		{
			RoleEntity entity = request.TranslateToEntity();

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<RoleEntity>(AuthAdminDatabase, RolesCollection);

			entity.RootParentId = entity.RootParentId == 0 ? entity.Id : entity.RootParentId;
			entity.ParentId = entity.ParentId == 0 ? entity.Id : entity.ParentId;
			entity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, entity);

			// add to parent child objects
			RoleEntity parentEntity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, entity.ParentId);
			if (parentEntity != null)
			{
				parentEntity.ChildRoleIds.Add(entity.Id);
				parentEntity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, parentEntity);
			}

			return entity.TranslateToResponse();
		}

		public Role Put(Role request)
		{
			RoleEntity currentEntity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, request.Id);

			RoleEntity entity = request.TranslateToEntity();

			entity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, entity);

			// If parentid different from entity remove from current parent entity and add to new parent entity
			if (currentEntity.ParentId != entity.ParentId)
			{
				//remove from current parent
				RoleEntity currentParentEntity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, currentEntity.ParentId);
				if (currentParentEntity != null)
				{
					currentParentEntity.ChildRoleIds.Remove(entity.Id);
					currentParentEntity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, currentParentEntity);
				}

				//add to new parent
				RoleEntity parentEntity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, entity.ParentId);
				if (parentEntity != null)
				{
					parentEntity.ChildRoleIds.Add(entity.Id);
					parentEntity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, parentEntity);
				}
			}

			return entity.TranslateToResponse();
		}

		public Role Delete(Role request)
		{
			GenericRepository.DeleteById<RoleEntity>(AuthAdminDatabase, RolesCollection, request.Id);

			//Delete from parent child list
			RoleEntity parentEntity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, request.ParentId);
			if (parentEntity != null)
			{
				parentEntity.ChildRoleIds.Remove(request.Id);
				parentEntity = GenericRepository.Add(AuthAdminDatabase, RolesCollection, parentEntity);
			}

			return null;
		}

		#endregion Role

		#region Roles

		public List<Role> Get(Roles role)
		{
			List<RoleEntity> entities = new List<RoleEntity>();

			entities = GenericRepository.GetList<RoleEntity>(AuthAdminDatabase, RolesCollection);

			if (entities == null)
				return null;

			return entities.TranslateToResponse();
		}

		#endregion Roles

		#region RoleTree

		public RoleTree Get(RoleTree roleTree)
		{
			//Get the rootparent entity
			RoleEntity entity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, roleTree.RootParentId);

			if (entity == null)
				return null;

			roleTree.TreeItems = new List<TreeItem>();
			TreeItem treeItem = new TreeItem();
			SetTreeItem(treeItem, entity);
			roleTree.TreeItems.Add(treeItem);

			return roleTree;
		}

		private void SetTreeItem(TreeItem treeItem, RoleEntity entity)
		{
			treeItem.Id = entity.Id;
			treeItem.Label = entity.Label;
			treeItem.RootParentId = entity.RootParentId;
			treeItem.ParentId = entity.ParentId;
			treeItem.Children = new List<TreeItem>();

			if (entity.ChildRoleIds != null)
			{
				treeItem.Children = new List<TreeItem>();
				foreach (var roleId in entity.ChildRoleIds)
				{
					RoleEntity roleEntity = GenericRepository.GetItemTopById<RoleEntity>(AuthAdminDatabase, RolesCollection, roleId);

					if (roleEntity != null)
					{
						TreeItem item = new TreeItem();
						SetTreeItem(item, roleEntity);
						treeItem.Children.Add(item);
					}
				}
			}
		}

		#endregion Roles
	}
}