using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public class EmployeeData
	{
		public static EmployeeEntity GetItemForInsert()
		{
			EmployeeEntity entity = new EmployeeEntity()
			{
				EntityId = ContactDetailsHelpers.CreateEntity().Id,
				PersonId = ContactDetailsHelpers.CreatePerson().Id,
				EmploymentStartDate = DateTime.UtcNow,
				EmploymentEndDate = DateTime.MinValue,
				DriversLicence = "dddd444444",
				Passport = "3434455555",
				HasVehicle = true,
				StaffAssociation = true,
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<EmployeeEntity> GetItemsForInsert()
		{
			List<EmployeeEntity> entities = new List<EmployeeEntity>();
			entities.Add(GetItemForInsert());
			EmployeeEntity entity = GetItemForInsert();
			entity.HasVehicle = false;
			entities.Add(entity);

			return entities;
		}
	}
}
