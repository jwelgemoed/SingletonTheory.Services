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
	public class TitleRepository
	{
		#region Fields & Properties

		private string _connectionStringName;

		#endregion Fields & Properties

		#region Constructors

		public TitleRepository(string connectionStringName)
		{
			if (string.IsNullOrEmpty(connectionStringName))
				throw new ArgumentNullException("connectionStringName");

			_connectionStringName = connectionStringName;

			using (IDatabaseProvider provider = ProviderFactory.GetProvider(connectionStringName))
			{
				if (!provider.CollectionExists(typeof(TitleEntity)))
					provider.CreateCollection(typeof(TitleEntity));
			}
		}

		#endregion Constructors

		#region Public Methods

		public TitleEntity Create(TitleEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Insert<TitleEntity>(entity);
			}
		}

		public List<TitleEntity> Create(List<TitleEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Create(entities[i]);
			}

			return entities;
		}

		public TitleEntity Read(long id)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.SelectById<TitleEntity>(id);
			}
		}

		public List<TitleEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				return provider.Select<TitleEntity>();
			}
		}

		public TitleEntity Update(TitleEntity entity)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				TitleEntity entityToUpdate = Read(entity.Id);
				if (entityToUpdate == null)
					throw new DataAccessException("Item not found"); //  This should not happen seeing that validation should check.

				entityToUpdate = UpdateProperties(entity, entityToUpdate);

				provider.Update<TitleEntity>(entityToUpdate);

				return entityToUpdate;
			}
		}

		public List<TitleEntity> Update(List<TitleEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i] = Update(entities[i]);
			}

			return entities;
		}

		public TitleEntity Delete(TitleEntity entity)
		{
			entity.DeletedDate = DateTime.UtcNow;

			return Update(entity);
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionStringName))
			{
				if (provider.CollectionExists(typeof(TitleEntity)))
					provider.DeleteAll<TitleEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private TitleEntity UpdateProperties(TitleEntity entity, TitleEntity entityToUpdate)
		{
			entityToUpdate.Description = entity.Description;
			entityToUpdate.DeletedDate = entity.DeletedDate;

			return entityToUpdate;
		}

		#endregion Private Methods
	}
}