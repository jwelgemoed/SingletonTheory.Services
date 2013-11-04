using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAccess;
using SingletonTheory.OrmLite;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.Services.AuthServices.Entities.Hours;

namespace SingletonTheory.Services.AuthServices.Repositories.Hours
{
	public class HourTypeRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public HourTypeRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(HourTypeEntity)))
					provider.CreateCollection(typeof(HourTypeEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public HourTypeEntity Create(HourTypeEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<HourTypeEntity>(entity);
			}
		}

		public List<HourTypeEntity> Create(List<HourTypeEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public HourTypeEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<HourTypeEntity>(id);
			}
		}

		public List<HourTypeEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<HourTypeEntity>();
			}
		}

		public HourTypeEntity Update(HourTypeEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				HourTypeEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Address not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<HourTypeEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<HourTypeEntity> Update(List<HourTypeEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public HourTypeEntity Delete(HourTypeEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(HourTypeEntity)))
					provider.DeleteAll<HourTypeEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private HourTypeEntity UpdateProperties(HourTypeEntity entity, HourTypeEntity entityToUpdate)
		{
			entityToUpdate.Description = entity.Description;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}