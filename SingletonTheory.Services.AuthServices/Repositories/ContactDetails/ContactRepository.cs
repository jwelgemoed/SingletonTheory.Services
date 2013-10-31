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
	public class ContactRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public ContactRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(ContactEntity)))
					provider.CreateCollection(typeof(ContactEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public ContactEntity Create(ContactEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<ContactEntity>(entity);
			}
		}

		public List<ContactEntity> Create(List<ContactEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public ContactEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<ContactEntity>(id);
			}
		}

		public List<ContactTypeEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<ContactTypeEntity>();
			}
		}

		public ContactEntity Update(ContactEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				ContactEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Item not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<ContactEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<ContactEntity> Update(List<ContactEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public ContactEntity Delete(ContactEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(ContactEntity)))
					provider.DeleteAll<ContactEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private ContactEntity UpdateProperties(ContactEntity entity, ContactEntity entityToUpdate)
		{
			entityToUpdate.ContactTypeId = entity.ContactTypeId;
			entityToUpdate.EntityId = entity.EntityId;
			entityToUpdate.Value = entity.Value;
			entityToUpdate.Preffered = entity.Preffered;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}