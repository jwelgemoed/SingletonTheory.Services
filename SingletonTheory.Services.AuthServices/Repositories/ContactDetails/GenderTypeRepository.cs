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
	public class GenderTypeRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public GenderTypeRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(GenderTypeEntity)))
					provider.CreateCollection(typeof(GenderTypeEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public GenderTypeEntity Create(GenderTypeEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<GenderTypeEntity>(entity);
			}
		}

		public List<GenderTypeEntity> Create(List<GenderTypeEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public GenderTypeEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<GenderTypeEntity>(id);
			}
		}

		public List<GenderTypeEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<GenderTypeEntity>();
			}
		}

		public GenderTypeEntity Update(GenderTypeEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				GenderTypeEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Item not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<GenderTypeEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<GenderTypeEntity> Update(List<GenderTypeEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public GenderTypeEntity Delete(GenderTypeEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(GenderTypeEntity)))
					provider.DeleteAll<GenderTypeEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private GenderTypeEntity UpdateProperties(GenderTypeEntity entity, GenderTypeEntity entityToUpdate)
		{
			entityToUpdate.Description = entity.Description;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}