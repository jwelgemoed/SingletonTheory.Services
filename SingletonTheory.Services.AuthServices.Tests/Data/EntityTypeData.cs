using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class EntityTypeData
	{
		public static EntityTypeEntity GetItemForInsert()
		{
			EntityTypeEntity entity = new EntityTypeEntity()
			{
				Description = "EntityType1",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<EntityTypeEntity> GetItemsForInsert()
		{
			List<EntityTypeEntity> entities = new List<EntityTypeEntity>();
			entities.Add(GetItemForInsert());
			EntityTypeEntity entity = GetItemForInsert();
			entity.Description = "EntityType2";
			entities.Add(entity);

			return entities;
		}
	}
}
