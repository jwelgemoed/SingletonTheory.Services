using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Helpers.Data
{
	public static class OccupationNameData
	{
		public static OccupationNameEntity GetItemForInsert()
		{
			OccupationNameEntity entity = new OccupationNameEntity()
			{
				Description = "Tekenaar",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		public static List<OccupationNameEntity> GetItemsForInsert()
		{
			List<OccupationNameEntity> entities = new List<OccupationNameEntity>();
			entities.Add(GetItemForInsert());
			OccupationNameEntity entity = GetItemForInsert();
			entity.Description = "Tekenaar2";
			entities.Add(entity);

			return entities;
		}
	}
}
