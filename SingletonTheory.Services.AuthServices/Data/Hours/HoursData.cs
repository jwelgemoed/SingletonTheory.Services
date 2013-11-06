using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.IO;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.Hours;
using SingletonTheory.Services.AuthServices.Repositories.Hours;

namespace SingletonTheory.Services.AuthServices.Data.Hours
{
	public class HoursData
	{
		public static void CreateData()
		{
			CreateHourTypes();
			CreateCostCentres();
		}

		public static void CreateHourTypes()
		{
			HourTypeRepository repository = new HourTypeRepository(ConfigSettings.HoursDatabaseConnectionName);
			var hourTypes = repository.Read();
			if (!CheckHourType(hourTypes, "BOOK"))
			{
				repository.Create(new HourTypeEntity
				                  {
					                  LookupCode = "BOOK",
														Description = "Geboekt",
														DeletedDate = DateTime.MinValue
				                  });
			}
			if (!CheckHourType(hourTypes, "BUDG"))
			{
				repository.Create(new HourTypeEntity
				{
					LookupCode = "BUDG",
					Description = "Gebudgeteerd",
					DeletedDate = DateTime.MinValue
				});
			}
			if (!CheckHourType(hourTypes, "PLAN"))
			{
				repository.Create(new HourTypeEntity
				{
					LookupCode = "PLAN",
					Description = "Gepland",
					DeletedDate = DateTime.MinValue
				});
			}
		}

		public static bool CheckHourType(List<HourTypeEntity> hourTypes, string lookUpCode )
		{
			foreach (var hourTypeEntity in hourTypes)
			{
				if (hourTypeEntity.LookupCode == lookUpCode)
					return true;
			}
			return false;
		}

		public static void CreateCostCentres()
		{
			CostCentreRepository repository = new CostCentreRepository(ConfigSettings.HoursDatabaseConnectionName);
			var costCentres = repository.Read();
			if (!CheckCostCentre(costCentres, "ENGI"))
			{
				repository.Create(new CostCentreEntity
				{
					LookupCode = "ENGI",
					Code = "01",
					Description = "Engineering",
					DeletedDate = DateTime.MinValue
				});
			}
			if (!CheckCostCentre(costCentres, "PROD"))
			{
				repository.Create(new CostCentreEntity
				{
					LookupCode = "PROD",
					Code = "02",
					Description = "Fabricage",
					DeletedDate = DateTime.MinValue
				});
			}
			if (!CheckCostCentre(costCentres, "PNTG"))
			{
				repository.Create(new CostCentreEntity
				{
					LookupCode = "PNTG",
					Code = "03",
					Description = "Spuiterij",
					DeletedDate = DateTime.MinValue
				});
			}
			if (!CheckCostCentre(costCentres, "ONBR"))
			{
				repository.Create(new CostCentreEntity
				{
					LookupCode = "ONBR",
					Code = "04",
					Description = "Boord",
					DeletedDate = DateTime.MinValue
				});
			}
		}

		public static bool CheckCostCentre(List<CostCentreEntity> costCentres, string lookUpCode)
		{
			foreach (var costCentreEntity in costCentres)
			{
				if (costCentreEntity.LookupCode == lookUpCode)
					return true;
			}
			return false;
		}
	}
}