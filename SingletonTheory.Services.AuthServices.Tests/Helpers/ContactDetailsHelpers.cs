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
			AddressTypeEntity entity = AddressTypeData.GetAddressTypeForInsert();
			repository.ClearCollection();

			entity = repository.Create(entity);

			return entity;
		}
	}
}
