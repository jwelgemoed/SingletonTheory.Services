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
	public class PersonRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public PersonRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(PersonEntity)))
					provider.CreateCollection(typeof(PersonEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public PersonEntity Create(PersonEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<PersonEntity>(entity);
			}
		}

		public List<PersonEntity> Create(List<PersonEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public PersonEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<PersonEntity>(id);
			}
		}

		public List<PersonEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<PersonEntity>();
			}
		}

		public PersonEntity Update(PersonEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				PersonEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Item not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<PersonEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<PersonEntity> Update(List<PersonEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public PersonEntity Delete(PersonEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(PersonEntity)))
					provider.DeleteAll<PersonEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private PersonEntity UpdateProperties(PersonEntity entity, PersonEntity entityToUpdate)
		{
			entityToUpdate.EntityId = entity.EntityId;
			entityToUpdate.OccupationNameId = entity.OccupationNameId;
			entityToUpdate.TitleId = entity.TitleId;
			entityToUpdate.SurnamePrefix = entity.SurnamePrefix;
			entityToUpdate.Surname = entity.Surname;
			entityToUpdate.MaidenNamePrefix = entity.MaidenNamePrefix;
			entityToUpdate.Nationality = entity.Nationality;
			entityToUpdate.DateOfBirth = entity.DateOfBirth;
			entityToUpdate.PlaceOfBirth = entity.PlaceOfBirth;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}