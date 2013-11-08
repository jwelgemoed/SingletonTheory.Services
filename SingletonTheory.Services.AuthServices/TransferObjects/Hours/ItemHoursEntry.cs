using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using SingletonTheory.Services.AuthServices.TransferObjects.Types;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Hours
{
	[Route("/hoursmanagement/itementry")]
	//[RequiredPermission(ApplyTo.Get, "ItemHoursEntry_Get")]
	//[RequiredPermission(ApplyTo.Post, "ItemHoursEntry_Post")]
	//[RequiredPermission(ApplyTo.Put, "ItemHoursEntry_Put")]
	//[RequiredPermission(ApplyTo.Delete, "ItemHoursEntry_Delete")]
	public class ItemHoursEntry:IReturn<ItemHoursEntry>
	{
		public long Id { get; set; }
		public long HourTypeId { get; set; }
		public HourType HourType { get; set; }
		public long CostCentreId { get; set; }
		public CostCentre CostCentre { get; set; }
		public int OrderNumber { get; set; }
		public int RoomNumber { get; set; }
		public int ItemNumber { get; set; }
		public int ParentItemNumber { get; set; }
		public int PersonNumber { get; set; }
		public decimal Hours { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public bool Deleted { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}