using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAccess;
using SingletonTheory.OrmLite;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Repositories.ContactDetails
{
	public class EntityTypeRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public EntityTypeRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(EntityTypeEntity)))
					provider.CreateCollection(typeof(EntityTypeEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public EntityTypeEntity Create(EntityTypeEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<EntityTypeEntity>(entity);
			}
		}

		public List<EntityTypeEntity> Create(List<EntityTypeEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public EntityTypeEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<EntityTypeEntity>(id);
			}
		}

		public List<EntityTypeEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<EntityTypeEntity>();
			}
		}

		public EntityTypeEntity Update(EntityTypeEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				EntityTypeEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Item not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<EntityTypeEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<EntityTypeEntity> Update(List<EntityTypeEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public EntityTypeEntity Delete(EntityTypeEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(EntityTypeEntity)))
					provider.DeleteAll<EntityTypeEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private EntityTypeEntity UpdateProperties(EntityTypeEntity entity, EntityTypeEntity entityToUpdate)
		{
			entityToUpdate.Description = entity.Description;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}