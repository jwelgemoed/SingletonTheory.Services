﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Helpers.Helpers;

namespace SingletonTheory.Services.AuthServices.Helpers.Data
{
	public static class EntityRelationshipData
	{
		public static EntityRelationshipEntity GetItemForInsert()
		{
			EntityRelationshipEntity entity = new EntityRelationshipEntity()
			{
				Entity1Id = ContactDetailsHelpers.CreateEntity().Id,
				Entity2Id = ContactDetailsHelpers.CreateEntity().Id,
				InDate = DateTime.UtcNow,
				OutDate = DateTime.UtcNow.AddDays(30),
				Preferred = true,
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		public static List<EntityRelationshipEntity> GetItemsForInsert()
		{
			List<EntityRelationshipEntity> entities = new List<EntityRelationshipEntity>();
			entities.Add(GetItemForInsert());
			EntityRelationshipEntity entity = GetItemForInsert();
			entity.Preferred = false;
			entities.Add(entity);

			return entities;
		}
	}
}
