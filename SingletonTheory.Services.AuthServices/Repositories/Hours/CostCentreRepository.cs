using System;
using System.Collections.Generic;
using ServiceStack.DataAccess;
using SingletonTheory.OrmLite;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.Services.AuthServices.Entities.Hours;

namespace SingletonTheory.Services.AuthServices.Repositories.Hours
{
	public class CostCentreRepository
	{
			#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public CostCentreRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(CostCentreEntity)))
					provider.CreateCollection(typeof(CostCentreEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public CostCentreEntity Create(CostCentreEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<CostCentreEntity>(entity);
			}
		}

		public List<CostCentreEntity> Create(List<CostCentreEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public CostCentreEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<CostCentreEntity>(id);
			}
		}

		public List<CostCentreEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<CostCentreEntity>();
			}
		}

		public CostCentreEntity Update(CostCentreEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				CostCentreEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Address not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<CostCentreEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<CostCentreEntity> Update(List<CostCentreEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public CostCentreEntity Delete(CostCentreEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(CostCentreEntity)))
					provider.DeleteAll<CostCentreEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private CostCentreEntity UpdateProperties(CostCentreEntity entity, CostCentreEntity entityToUpdate)
		{
			entityToUpdate.Code = entity.Code;
			entityToUpdate.LookupCode = entity.LookupCode;
			entityToUpdate.Description = entity.Description;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}