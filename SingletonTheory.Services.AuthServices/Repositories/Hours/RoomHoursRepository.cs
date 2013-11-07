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
	public class RoomHoursRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public RoomHoursRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(RoomHoursEntity)))
					provider.CreateCollection(typeof(RoomHoursEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public RoomHoursEntity Create(RoomHoursEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<RoomHoursEntity>(entity);
			}
		}

		public List<RoomHoursEntity> Create(List<RoomHoursEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public RoomHoursEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<RoomHoursEntity>(id);
			}
		}

		public List<RoomHoursEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<RoomHoursEntity>();
			}
		}

		public RoomHoursEntity Update(RoomHoursEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				RoomHoursEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Address not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<RoomHoursEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<RoomHoursEntity> Update(List<RoomHoursEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public RoomHoursEntity Delete(RoomHoursEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(RoomHoursEntity)))
					provider.DeleteAll<RoomHoursEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private RoomHoursEntity UpdateProperties(RoomHoursEntity entity, RoomHoursEntity entityToUpdate)
		{
			entityToUpdate.HourTypeId = entity.HourTypeId;
			entityToUpdate.CostCentreId = entity.CostCentreId;
			entityToUpdate.ConceptNumber = entity.ConceptNumber;
			entityToUpdate.OrderNumber = entity.OrderNumber;
			entityToUpdate.RoomNumber = entity.RoomNumber;
			entityToUpdate.PersonNumber = entity.PersonNumber;
			entityToUpdate.Hours = entity.Hours;
			entityToUpdate.Description = entity.Description;
			entityToUpdate.Date = entity.Date;
			entityToUpdate.Deleted = entity.Deleted;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}