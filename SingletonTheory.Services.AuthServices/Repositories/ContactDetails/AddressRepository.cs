using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using ServiceStack.DataAccess;
using SingletonTheory.OrmLite;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Repositories.ContactDetails
{
	public class AddressRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public AddressRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(AddressEntity)))
					provider.CreateCollection(typeof(AddressEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public AddressEntity Create(AddressEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<AddressEntity>(entity);
			}
		}

		public List<AddressEntity> Create(List<AddressEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public AddressEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<AddressEntity>(id);
			}
		}

		public List<AddressEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<AddressEntity>();
			}
		}

		public AddressEntity Update(AddressEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				AddressEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Address not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<AddressEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<AddressEntity> Update(List<AddressEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public AddressEntity Delete(AddressEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(AddressEntity)))
					provider.DeleteAll<AddressEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private AddressEntity UpdateProperties(AddressEntity entity, AddressEntity entityToUpdate)
		{
			entityToUpdate.AddressTypeId = entity.AddressTypeId;
			entityToUpdate.EntityId = entity.EntityId;
			entityToUpdate.Street = entity.Street;
			entityToUpdate.StreetNumber = entity.StreetNumber;
			entityToUpdate.StreetNumberAddition = entity.StreetNumberAddition;
			entityToUpdate.PostalCode = entity.PostalCode;
			entityToUpdate.City = entity.City;
			entityToUpdate.CountryCode = entity.CountryCode;
			entityToUpdate.Preferred = entity.Preferred;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}