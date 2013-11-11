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
	public class EntityRelationshipRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public EntityRelationshipRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(EntityRelationshipEntity)))
					provider.CreateCollection(typeof(EntityRelationshipEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public EntityRelationshipEntity Create(EntityRelationshipEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<EntityRelationshipEntity>(entity);
			}
		}

		public List<EntityRelationshipEntity> Create(List<EntityRelationshipEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public EntityRelationshipEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<EntityRelationshipEntity>(id);
			}
		}

		public List<EntityRelationshipEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<EntityRelationshipEntity>();
			}
		}

		public EntityRelationshipEntity Update(EntityRelationshipEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				EntityRelationshipEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Item not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<EntityRelationshipEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<EntityRelationshipEntity> Update(List<EntityRelationshipEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public EntityRelationshipEntity Delete(EntityRelationshipEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(EntityRelationshipEntity)))
					provider.DeleteAll<EntityRelationshipEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private EntityRelationshipEntity UpdateProperties(EntityRelationshipEntity entity, EntityRelationshipEntity entityToUpdate)
		{
			entityToUpdate.Entity1Id = entity.Entity1Id;
			entityToUpdate.Entity2Id = entity.Entity2Id;
			entityToUpdate.InDate = entity.InDate;
			entityToUpdate.OutDate = entity.OutDate;
			entityToUpdate.Preferred = entity.Preferred;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}