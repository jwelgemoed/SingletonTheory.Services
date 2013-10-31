using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Entities.Hours;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	public static class HoursData
	{
		public static CostCentreEntity GetCostCentreForInsert()
		{
			CostCentreEntity entity = new CostCentreEntity()
			{
				Code = "89",
				Description = "Test Cost Centre",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<CostCentreEntity> GetCostCentresForInsert()
		{
			List<CostCentreEntity> entities = new List<CostCentreEntity>();
			entities.Add(GetCostCentreForInsert());
			CostCentreEntity entity = GetCostCentreForInsert();
			entity.Code = "88";
			entities.Add(entity);

			return entities;
		}

		public static HourTypeEntity GetHourTypeForInsert()
		{
			HourTypeEntity entity = new HourTypeEntity()
			{
				Description = "Budgeted",
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<HourTypeEntity> GetHourTypesForInsert()
		{
			List<HourTypeEntity> entities = new List<HourTypeEntity>();
			entities.Add(GetHourTypeForInsert());
			HourTypeEntity entity = GetHourTypeForInsert();
			entity.Description = "Planned";
			entities.Add(entity);

			return entities;
		}
	}
}
