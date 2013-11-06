using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail;

namespace SingletonTheory.Services.AuthServices.Data
{
	public class ContactDetailsData
	{
		static List<OccupationNameEntity> _occupationNameEntities = new List<OccupationNameEntity>();
		private static List<EntityTypeEntity> _entityTypeEntities = new List<EntityTypeEntity>();
		private static List<ContactTypeEntity> _contactTypeEntities = new List<ContactTypeEntity>();
		private static List<TitleEntity> _titleEntities = new List<TitleEntity>();
		private static List<ContactEntity> _contactEntities = new List<ContactEntity>();
		private static List<EntityEntity> _entityEntities = new List<EntityEntity>();
		private static List<PersonEntity> _personEntities = new List<PersonEntity>();

		public static void CreateData()
		{
			CreateTitles();
			CreateContactTypes();
			CreateEntityTypes();
			CreateOccupationNames();
			CreateContactEntityPerson();
		}

		private static void CreateContactEntityPerson()
		{
			CreateContacts();
			CreateEntities();
			CreatePerson();
		}

		private static void CreateEntities()
		{
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();

			EntityEntity entity = new EntityEntity()
			{
				EntityTypeId = _entityTypeEntities[0].Id,
				Name = "EntityType1",
				DeletedDate = DateTime.MinValue
			};
			EntityEntity mEntity = new EntityEntity()
			{
				EntityTypeId = _entityTypeEntities[1].Id,
				Name = "EntityType1",
				DeletedDate = DateTime.MinValue
			};

			entity = repository.Create(entity);
			mEntity = repository.Create(mEntity);

			_entityEntities.Add(entity);
			_entityEntities.Add(mEntity);
		}

		private static void CreatePerson()
		{
			PersonRepository repository = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();

			PersonEntity entity = new PersonEntity()
			{
				EntityId = _entityTypeEntities[0].Id,
				OccupationNameId = _occupationNameEntities[0].Id,
				TitleId = _titleEntities[0].Id,
				SurnamePrefix = "van",
				Surname = "Riebeeck",
				MaidenNamePrefix = string.Empty,
				Nationality = "nl",
				DateOfBirth = DateTime.UtcNow.AddYears(-34),
				PlaceOfBirth = "Houten",
				DeletedDate = DateTime.MinValue
			};
			PersonEntity mEntity = new PersonEntity()
			{
				EntityId = _entityTypeEntities[1].Id,
				OccupationNameId = _occupationNameEntities[1].Id,
				TitleId = _titleEntities[1].Id,
				SurnamePrefix = string.Empty,
				Surname = "Stel",
				MaidenNamePrefix = "",
				Nationality = "BL",
				DateOfBirth = DateTime.UtcNow.AddYears(-37),
				PlaceOfBirth = "Zeist",
				DeletedDate = DateTime.MinValue
			};

			entity = repository.Create(entity);
			mEntity = repository.Create(mEntity);

			_personEntities.Add(entity);
			_personEntities.Add(mEntity);
		}

		private static void CreateContacts()
		{
			ContactRepository repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();

			ContactEntity entity = new ContactEntity()
			{
				ContactTypeId = _contactTypeEntities[0].Id,
				Value = "value1",
				Preffered = true,
				DeletedDate = DateTime.MinValue
			};
			ContactEntity mEntity = new ContactEntity()
			{
				ContactTypeId = _contactTypeEntities[1].Id,
				Value = "value1",
				Preffered = true,
				DeletedDate = DateTime.MinValue
			};

			entity = repository.Create(entity);
			mEntity = repository.Create(mEntity);

			_contactEntities.Add(entity);
			_contactEntities.Add(mEntity);
		}

		private static void CreateOccupationNames()
		{
			OccupationNameRepository repository = new OccupationNameRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
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
			_occupationNameEntities.Add(entity);
			_occupationNameEntities.Add(mEntity);
		}

		private static void CreateEntityTypes()
		{
			EntityTypeRepository repository = new EntityTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
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

			_entityTypeEntities.Add(entity);
			_entityTypeEntities.Add(mEntity);
		}

		private static void CreateContactTypes()
		{
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
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

			_contactTypeEntities.Add(entity);
			_contactTypeEntities.Add(mEntity);
		}

		private static void CreateTitles()
		{
			TitleRepository repository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
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

			entity = repository.Create(entity);
			mEntity = repository.Create(mEntity);

			_titleEntities.Add(entity);
			_titleEntities.Add(mEntity);
		}
	}
}