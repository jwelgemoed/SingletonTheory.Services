using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funq;
using ServiceStack.Common;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Extensions;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin;
using SingletonTheory.Services.AuthServices.Utilities;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class DomainPermissionService : Service
	{
		private UserRepository _userRepository;

		#region Constants

		private const string AuthAdminDatabase = "AuthAdminDatabase";
		private const string DomainPermissionsCollection = "DomainPermissions";

		#endregion Constants

		#region DomainPermission

		public DomainPermission Get(DomainPermission request)
		{
			if (request.Id != 0)
			{
				DomainPermissionEntity entity = GenericRepository.GetItemTopById<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection, request.Id);

				if (entity == null)
					return null;

				return entity.TranslateToResponse();
			}

			return null;
		}

		public DomainPermission Post(DomainPermission request)
		{
			DomainPermissionEntity entity = request.TranslateToEntity();

			if (entity.Id == 0)
				entity.Id = GenericRepository.GetMaxIdIncrement<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection);

			entity = GenericRepository.Add(AuthAdminDatabase, DomainPermissionsCollection, entity);

			return entity.TranslateToResponse();
		}

		public DomainPermission Put(DomainPermission request)
		{
			DomainPermissionEntity entity = request.TranslateToEntity();

			entity = GenericRepository.Add(AuthAdminDatabase, DomainPermissionsCollection, entity);

			return entity.TranslateToResponse();
		}

		public DomainPermission Delete(DomainPermission request)
		{
			GenericRepository.DeleteById<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection, request.Id);

			return null;
		}

		#endregion DomainPermission

		#region DomainPermissions

		public List<DomainPermission> Get(DomainPermissions role)
		{
			List<DomainPermissionEntity> entities = new List<DomainPermissionEntity>();
			List<DomainPermissionEntity> entitiesToReturn = new List<DomainPermissionEntity>();

			entities = GenericRepository.GetList<DomainPermissionEntity>(AuthAdminDatabase, DomainPermissionsCollection);

			if (entities == null)
				return null;

			IAuthSession session = this.GetSession();
			
			if (_userRepository == null)
			{
				// TODO:  Inject UserRepository from Top Level
				Container container = EndpointHost.Config.ServiceManager.Container;
				_userRepository = container.Resolve<UserRepository>();
			}

			UserEntity userEntity = _userRepository.Read(session.UserName);
			
			if (userEntity != null)
			{
				List<int> domainPermissionIds = PermissionUtility.GetDomainPermissionIdsForRoleId(userEntity.Roles);

				foreach (var domainPermissionEntity in entities)
				{
					if (domainPermissionIds.Contains(domainPermissionEntity.Id))
					{
						entitiesToReturn.Add(domainPermissionEntity);
					}
				}
			}

			return entitiesToReturn.TranslateToResponse();
		}

		#endregion DomainPermissions

	}
}