using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class AddressTypeData
	{
		public static AddressTypeEntity GetAddressTypeForInsert()
		{
			AddressTypeEntity entity = new AddressTypeEntity()
			{
				Description = "Home",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<AddressTypeEntity> GetAddressTypesForInsert()
		{
			List<AddressTypeEntity> entities = new List<AddressTypeEntity>();
			entities.Add(GetAddressTypeForInsert());
			AddressTypeEntity entity = GetAddressTypeForInsert();
			entity.Description = "Work";
			entities.Add(entity);

			return entities;
		}
	}
}
