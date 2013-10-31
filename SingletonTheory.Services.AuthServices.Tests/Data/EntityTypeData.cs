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
				Description = "xxx",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<EntityTypeEntity> GetItemsForInsert()
		{
			List<EntityTypeEntity> entities = new List<EntityTypeEntity>();
			entities.Add(GetItemForInsert());
			EntityTypeEntity entity = GetItemForInsert();
			entity.Description = "yyy";
			entities.Add(entity);

			return entities;
		}
	}
}
