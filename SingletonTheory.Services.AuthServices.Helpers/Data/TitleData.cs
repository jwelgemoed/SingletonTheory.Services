using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Helpers.Data
{
	public static class TitleData
	{
		public static TitleEntity GetItemForInsert()
		{
			TitleEntity entity = new TitleEntity()
			{
				Description = "Dhr",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		public static List<TitleEntity> GetItemsForInsert()
		{
			List<TitleEntity> entities = new List<TitleEntity>();
			entities.Add(GetItemForInsert());
			TitleEntity entity = GetItemForInsert();
			entity.Description = "Mvr";
			entities.Add(entity);

			return entities;
		}
	}
}