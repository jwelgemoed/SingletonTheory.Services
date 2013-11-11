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
				LookupCode = "TCCC",
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
			entity.LookupCode = "TCCN";
			entities.Add(entity);

			return entities;
		}

		public static HourTypeEntity GetHourTypeForInsert()
		{
			HourTypeEntity entity = new HourTypeEntity()
			{
				LookupCode = "BUDG",
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
			entity.LookupCode = "BUDD";
			entities.Add(entity);

			return entities;
		}

		public static ItemHoursEntity GetItemHoursEntryForInsert(HourTypeEntity hourType, CostCentreEntity costCentre)
		{
			ItemHoursEntity entity = new ItemHoursEntity()
			{
				HourTypeId = hourType.Id,
				CostCentreId = costCentre.Id,
				OrderNumber = 1234,
				RoomNumber = 67,
				ItemNumber = 5678,
				ParentItemNumber = 8765,
				PersonNumber = 123456789,
				Description = "Work on Monday",
				Date = DateTime.UtcNow,
				Deleted = true,
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<ItemHoursEntity> GetItemHoursEntriesForInsert(HourTypeEntity hourType, CostCentreEntity costCentre)
		{
			List<ItemHoursEntity> entities = new List<ItemHoursEntity>();
			entities.Add(GetItemHoursEntryForInsert(hourType, costCentre));
			ItemHoursEntity entity = GetItemHoursEntryForInsert(hourType, costCentre);
			entity.OrderNumber = 1111;
			entities.Add(entity);

			return entities;
		}

		public static RoomHoursEntity GetRoomHoursEntryForInsert(HourTypeEntity hourType, CostCentreEntity costCentre)
		{
			RoomHoursEntity entity = new RoomHoursEntity()
			{
				HourTypeId = hourType.Id,
				CostCentreId = costCentre.Id,
				ConceptNumber = 9999,
				OrderNumber = 1234,
				RoomNumber = 67,
				PersonNumber = 123456789,
				Description = "Work on Monday",
				DeliveryDate = DateTime.UtcNow,
				Deleted = true,
				DeletedDate = DateTime.MinValue
			};

			return entity;
		}

		internal static List<RoomHoursEntity> GetRoomHoursEntriesForInsert(HourTypeEntity hourType, CostCentreEntity costCentre)
		{
			List<RoomHoursEntity> entities = new List<RoomHoursEntity>();
			entities.Add(GetRoomHoursEntryForInsert(hourType, costCentre));
			RoomHoursEntity entity = GetRoomHoursEntryForInsert(hourType, costCentre);
			entity.OrderNumber = 1111;
			entities.Add(entity);

			return entities;
		}
	}
}
