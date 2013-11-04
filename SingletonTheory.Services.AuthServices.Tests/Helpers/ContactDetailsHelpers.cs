using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.Tests.Data;

namespace SingletonTheory.Services.AuthServices.Tests.Helpers
{
	public static class ContactDetailsHelpers
	{
		public static AddressTypeEntity CreateAddressType()
		{
			AddressTypeRepository repository = new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressTypeEntity entity = AddressTypeData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static void ClearAddressType()
		{
			AddressTypeRepository repository = new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
		}

		public static EntityEntity CreateEntity()
		{
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityEntity entity = EntityData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static void ClearEntity()
		{
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
		}

		public static EntityTypeEntity CreateEntityType()
		{
			EntityTypeRepository repository = new EntityTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityTypeEntity entity = EntityTypeData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static void ClearEntityType()
		{
			EntityTypeRepository repository = new EntityTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
		}

		public static ContactEntity CreateContact()
		{
			ContactRepository repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactEntity entity = ContactData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static void ClearContact()
		{
			ContactRepository repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
		}

		public static ContactTypeEntity CreateContactType()
		{
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactTypeEntity entity = ContactTypeData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static void ClearContactType()
		{
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
		}

		public static PersonEntity CreatePerson()
		{
			PersonRepository repository = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
			PersonEntity entity = PersonData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static void ClearPerson()
		{
			PersonRepository repository = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
		}

		public static OccupationNameEntity CreateOccupationName()
		{
			OccupationNameRepository repository = new OccupationNameRepository(ConfigSettings.MySqlDatabaseConnectionName);
			OccupationNameEntity entity = OccupationNameData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static void ClearOccupationName()
		{
			OccupationNameRepository repository = new OccupationNameRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
		}

		public static TitleEntity CreateTitle()
		{
			TitleRepository repository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			TitleEntity entity = TitleData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static void ClearTitle()
		{
			TitleRepository repository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();
		}
	}
}
