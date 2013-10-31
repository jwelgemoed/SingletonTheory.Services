using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class ContactTypeData
	{
		public static ContactTypeEntity GetItemForInsert()
		{
			ContactTypeEntity entity = new ContactTypeEntity()
			{
				Description = "Type1",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<ContactTypeEntity> GetItemsForInsert()
		{
			List<ContactTypeEntity> entities = new List<ContactTypeEntity>();
			entities.Add(GetItemForInsert());
			ContactTypeEntity entity = GetItemForInsert();
			entity.Description = "Type2";
			entities.Add(entity);

			return entities;
		}
	}
}
