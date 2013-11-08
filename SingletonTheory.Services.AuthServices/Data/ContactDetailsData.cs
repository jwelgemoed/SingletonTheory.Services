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
		private static List<AddressTypeEntity> _addressTypeEntities = new List<AddressTypeEntity>();
		private static List<AddressEntity> _addressEntities = new List<AddressEntity>();

		public static void CreateData()
		{
			//Todo: there are better ways to do this if you have time
			ClearAll();

			CreateTitles();
			CreateContactTypes();
			CreateEntityTypes();
			CreateOccupationNames();
			CreateEntities();
			CreateContacts();
			CreatePerson();

			CreateAddressTypes();
		}

		private static void ClearAll()
		{
			PersonRepository repository1 = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactRepository repository2 = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityRepository repository3 = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			OccupationNameRepository repository4 = new OccupationNameRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityTypeRepository repository5 = new EntityTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactTypeRepository repository6 = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			TitleRepository repository7 = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressTypeRepository repository8 = new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressRepository repository9 = new AddressRepository(ConfigSettings.MySqlDatabaseConnectionName);

			repository9.ClearCollection();
			repository1.ClearCollection();
			repository2.ClearCollection();
			repository3.ClearCollection();
			repository4.ClearCollection();
			repository5.ClearCollection();
			repository6.ClearCollection();
			repository7.ClearCollection();
			repository8.ClearCollection();

		}

		private static void CreateEntities()
		{
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);

			EntityEntity entity = new EntityEntity()
			{
				EntityTypeId = _entityTypeEntities[0].Id,
				Name = "EntityType1",
				DeletedDate = DateTime.MinValue
			};
			EntityEntity mEntity = new EntityEntity()
			{
				EntityTypeId = _entityTypeEntities[1].Id,
				Name = "EntityType2",
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

			PersonEntity entity = new PersonEntity()
			{
				EntityId = _entityEntities[0].Id,
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
				EntityId = _entityEntities[1].Id,
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

			ContactEntity entity = new ContactEntity()
			{
				ContactTypeId = _contactTypeEntities[0].Id,
				EntityId = _entityEntities[0].Id,
				Value = "value1",
				Preffered = true,
				DeletedDate = DateTime.MinValue
			};
			ContactEntity mEntity = new ContactEntity()
			{
				ContactTypeId = _contactTypeEntities[1].Id,
				EntityId = _entityEntities[1].Id,
				Value = "value2",
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

		private static void CreateAddressTypes()
		{
			AddressTypeRepository repository = new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);

			AddressTypeEntity entity = new AddressTypeEntity()
			{
				Description = "Home",
				DeletedDate = DateTime.MinValue
			};

			AddressTypeEntity mEntity = new AddressTypeEntity()
			{
				Description = "Work",
				DeletedDate = DateTime.MinValue
			};

			entity = repository.Create(entity);
			mEntity = repository.Create(mEntity);

			_addressTypeEntities.Add(entity);
			_addressTypeEntities.Add(mEntity);
		}

		private static void CreateTitles()
		{
			TitleRepository repository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			
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