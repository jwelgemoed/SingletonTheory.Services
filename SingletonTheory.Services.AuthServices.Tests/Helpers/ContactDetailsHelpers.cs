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
			repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static EntityEntity CreateEntity()
		{
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityEntity entity = EntityData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static EntityTypeEntity CreateEntityType()
		{
			EntityTypeRepository repository = new EntityTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityTypeEntity entity = EntityTypeData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static ContactEntity CreateContact()
		{
			ContactRepository repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactEntity entity = ContactData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}

		public static ContactTypeEntity CreateContactType()
		{
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactTypeEntity entity = ContactTypeData.GetItemForInsert();
			//repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}
	}
}
