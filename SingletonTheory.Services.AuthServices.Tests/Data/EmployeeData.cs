using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public class EmployeeData
	{
		public static EmployeeEntity GetItemForInsert()
		{
			EmployeeEntity entity = new EmployeeEntity()
			{
				HasVehicle = true,
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
