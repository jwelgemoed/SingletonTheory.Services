using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class AddressData
	{
		public static AddressEntity GetAddressForInsert()
		{
			AddressEntity entity = new AddressEntity()
			{
				AddressTypeId = 1,
				EntityId = 1,
				Street = "Bonga",
				StreetNumber = "6",
				StreetNumberAddition = "F",
				PostalCode = "3991TS",
				City = "Houten",
				CountryCode = "NL",
				Preferred = true,
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<AddressEntity> GetAddressesForInsert()
		{
			List<AddressEntity> entities = new List<AddressEntity>();
			entities.Add(GetAddressForInsert());
			AddressEntity entity = GetAddressForInsert();
			entity.Street = "SomeOtherstreet";
			entities.Add(entity);

			return entities;
		}
	}
}
