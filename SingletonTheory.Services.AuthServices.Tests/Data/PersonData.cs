using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class PersonData
	{
		public static PersonEntity GetItemForInsert()
		{
			PersonEntity entity = new PersonEntity()
			{
				Surname = "SurnamePerson1",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<PersonEntity> GetItemsForInsert()
		{
			List<PersonEntity> entities = new List<PersonEntity>();
			entities.Add(GetItemForInsert());
			PersonEntity entity = GetItemForInsert();
			entity.Surname = "SurnamePerson2";
			entities.Add(entity);

			return entities;
		}
	}
}