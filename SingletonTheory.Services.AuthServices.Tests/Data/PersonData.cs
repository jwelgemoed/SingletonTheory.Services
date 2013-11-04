using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class PersonData
	{
		public static PersonEntity GetItemForInsert()
		{
			PersonEntity entity = new PersonEntity()
			{
				EntityId = ContactDetailsHelpers.CreateEntity().Id,
				OccupationNameId = ContactDetailsHelpers.CreateOccupationName().Id,
				TitleId = ContactDetailsHelpers.CreateTitle().Id,
				SurnamePrefix = "Von",
				Surname = "SurnamePerson1",
				MaidenNamePrefix = "",
				Nationality = "NL",
				PlaceOfBirth = "Sasol",
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