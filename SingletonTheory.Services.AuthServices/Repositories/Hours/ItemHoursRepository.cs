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
	public class ItemHoursRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public ItemHoursRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(ItemHoursEntity)))
					provider.CreateCollection(typeof(ItemHoursEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public ItemHoursEntity Create(ItemHoursEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<ItemHoursEntity>(entity);
			}
		}

		public List<ItemHoursEntity> Create(List<ItemHoursEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public ItemHoursEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<ItemHoursEntity>(id);
			}
		}

		public List<ItemHoursEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<ItemHoursEntity>();
			}
		}

		public ItemHoursEntity Update(ItemHoursEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				ItemHoursEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Address not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<ItemHoursEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<ItemHoursEntity> Update(List<ItemHoursEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public ItemHoursEntity Delete(ItemHoursEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(ItemHoursEntity)))
					provider.DeleteAll<ItemHoursEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private ItemHoursEntity UpdateProperties(ItemHoursEntity entity, ItemHoursEntity entityToUpdate)
		{
			entityToUpdate.HourTypeId = entity.HourTypeId;
			entityToUpdate.CostCentreId = entity.CostCentreId;
			entityToUpdate.OrderNumber = entity.OrderNumber;
			entityToUpdate.RoomNumber = entity.RoomNumber;
			entityToUpdate.ItemNumber = entity.ItemNumber;
			entityToUpdate.ParentItemNumber = entity.ParentItemNumber;
			entityToUpdate.PersonNumber = entity.PersonNumber;
			entityToUpdate.Description = entity.Description;
			entityToUpdate.Date = entity.Date;
			entityToUpdate.Deleted = entity.Deleted;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}