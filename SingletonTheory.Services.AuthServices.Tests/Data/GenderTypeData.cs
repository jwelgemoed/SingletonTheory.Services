using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class GenderTypeData
	{
		public static GenderTypeEntity GetItemForInsert()
		{
			GenderTypeEntity entity = new GenderTypeEntity()
			{
				Description = "Male",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<GenderTypeEntity> GetItemsForInsert()
		{
			List<GenderTypeEntity> entities = new List<GenderTypeEntity>();
			entities.Add(GetItemForInsert());
			GenderTypeEntity entity = GetItemForInsert();
			entity.Description = "Female";
			entities.Add(entity);

			return entities;
		}
	}
}