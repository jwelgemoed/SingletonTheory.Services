using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Helpers.Helpers;

namespace SingletonTheory.Services.AuthServices.Helpers.Data
{
	public static class AddressData
	{
		public static AddressEntity GetItemForInsert()
		{
			AddressEntity entity = new AddressEntity()
			{
				AddressTypeId = ContactDetailsHelpers.CreateAddressType().Id,
				EntityId = ContactDetailsHelpers.CreateEntity().Id,
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

		public static List<AddressEntity> GetItemsForInsert()
		{
			List<AddressEntity> entities = new List<AddressEntity>();
			entities.Add(GetItemForInsert());
			AddressEntity entity = GetItemForInsert();
			entity.Street = "SomeOtherstreet";
			entities.Add(entity);

			return entities;
		}
	}
}
