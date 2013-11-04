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
	public class EmployeeRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public EmployeeRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(EmployeeEntity)))
					provider.CreateCollection(typeof(EmployeeEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public EmployeeEntity Create(EmployeeEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<EmployeeEntity>(entity);
			}
		}

		public List<EmployeeEntity> Create(List<EmployeeEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public EmployeeEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<EmployeeEntity>(id);
			}
		}

		public List<EmployeeEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<EmployeeEntity>();
			}
		}

		public EmployeeEntity Update(EmployeeEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				EmployeeEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Item not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<EmployeeEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<EmployeeEntity> Update(List<EmployeeEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public EmployeeEntity Delete(EmployeeEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(EmployeeEntity)))
					provider.DeleteAll<EmployeeEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private EmployeeEntity UpdateProperties(EmployeeEntity entity, EmployeeEntity entityToUpdate)
		{
			entityToUpdate.EntityId = entity.EntityId;
			entityToUpdate.PersonId = entity.PersonId;
			entityToUpdate.EmploymentStartDate = entity.EmploymentStartDate;
			entityToUpdate.EmploymentEndDate = entity.EmploymentEndDate;
			entityToUpdate.DriversLicence = entity.DriversLicence;
			entityToUpdate.Passport = entity.Passport;
			entityToUpdate.HasVehicle = entity.HasVehicle;
			entityToUpdate.StaffAssociation = entity.StaffAssociation;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}