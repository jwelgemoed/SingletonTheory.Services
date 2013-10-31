using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class EntityRelationshipData
	{
		public static EntityRelationshipEntity GetItemForInsert()
		{
			EntityRelationshipEntity entity = new EntityRelationshipEntity()
			{
				Preffered = true,
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<EntityRelationshipEntity> GetItemsForInsert()
		{
			List<EntityRelationshipEntity> entities = new List<EntityRelationshipEntity>();
			entities.Add(GetItemForInsert());
			EntityRelationshipEntity entity = GetItemForInsert();
			entity.Preffered = false;
			entities.Add(entity);

			return entities;
		}
	}
}
