using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Helpers.Data
{
	public static class AddressTypeData
	{
		public static AddressTypeEntity GetItemForInsert()
		{
			AddressTypeEntity entity = new AddressTypeEntity()
			{
				Description = "Home",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		public static List<AddressTypeEntity> GetItemsForInsert()
		{
			List<AddressTypeEntity> entities = new List<AddressTypeEntity>();
			entities.Add(GetItemForInsert());
			AddressTypeEntity entity = GetItemForInsert();
			entity.Description = "Work";
			entities.Add(entity);

			return entities;
		}
	}
}
