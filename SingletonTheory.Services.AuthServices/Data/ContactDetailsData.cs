using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Data
{
	public class ContactDetailsData
	{
		public static void CreateData()
		{
			CreateTitles();
			CreateContactTypes();
			CreateEntityTypes();
			CreateOccupationNames();
		}

		private static void CreateOccupationNames()
		{
			OccupationNameRepository repository = new OccupationNameRepository(ConfigSettings.MySqlDatabaseConnectionName);
			OccupationNameEntity entity = new OccupationNameEntity()
			{
				Description = "Tekenaar",
				DeletedDate = DateTime.MinValue
			};

			OccupationNameEntity mEntity = new OccupationNameEntity()
			{
				Description = "Ingeneur",
				DeletedDate = DateTime.MinValue
			};

			entity = repository.Create(entity);
			mEntity = repository.Create(mEntity);
		}

		private static void CreateEntityTypes()
		{
			EntityTypeRepository repository = new EntityTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityTypeEntity entity = new EntityTypeEntity()
			{
				Description = "Contact",
				DeletedDate = DateTime.MinValue
			};

			EntityTypeEntity mEntity = new EntityTypeEntity()
			{
				Description = "Employee",
				DeletedDate = DateTime.MinValue
			};

			entity = repository.Create(entity);
			mEntity = repository.Create(mEntity);
		}

		private static void CreateContactTypes()
		{
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactTypeEntity entity = new ContactTypeEntity()
			{
				Description = "Personal",
				DeletedDate = DateTime.MinValue
			};

			ContactTypeEntity mEntity = new ContactTypeEntity()
			{
				Description = "Suplier",
				DeletedDate = DateTime.MinValue
			};

			entity = repository.Create(entity);
			mEntity = repository.Create(mEntity);
		}

		private static void CreateTitles()
		{
			TitleRepository titleRepository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			TitleEntity entity = new TitleEntity()
			{
				Description = "Dhr",
				DeletedDate = DateTime.MinValue
			};
			TitleEntity mEntity = new TitleEntity()
			{
				Description = "Mvr",
				DeletedDate = DateTime.MinValue
			};

			entity = titleRepository.Create(entity);
			mEntity = titleRepository.Create(mEntity);
		}
	}
}