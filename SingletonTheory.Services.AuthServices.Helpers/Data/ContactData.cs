using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Helpers.Helpers;

namespace SingletonTheory.Services.AuthServices.Helpers.Data
{
	public static class ContactData
	{
		public static ContactEntity GetItemForInsert()
		{
			ContactEntity entity = new ContactEntity()
			{
				ContactTypeId = ContactDetailsHelpers.CreateContactType().Id,
				EntityId = ContactDetailsHelpers.CreateEntity().Id,
				Value = "Contact1",
				Preffered = true,
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		public static List<ContactEntity> GetItemsForInsert()
		{
			List<ContactEntity> entities = new List<ContactEntity>();
			entities.Add(GetItemForInsert());
			ContactEntity entity = GetItemForInsert();
			entity.Value = "Contact2";
			entities.Add(entity);

			return entities;
		}
	}
}
