using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Helpers.Helpers;

namespace SingletonTheory.Services.AuthServices.Helpers.Data
{
	public static class EntityData
	{
		public static EntityEntity GetItemForInsert()
		{
			EntityEntity entity = new EntityEntity()
			{
				Name = "Enitity1",
				EntityTypeId = ContactDetailsHelpers.CreateEntityType().Id,
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		public static List<EntityEntity> GetItemsForInsert()
		{
			List<EntityEntity> entities = new List<EntityEntity>();
			entities.Add(GetItemForInsert());
			EntityEntity entity = GetItemForInsert();
			entity.Name = "Enitity2";
			entities.Add(entity);

			return entities;
		}
	}
}
