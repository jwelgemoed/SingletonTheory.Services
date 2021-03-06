﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using SingletonTheory.Services.AuthServices.TransferObjects.Types;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Hours
{
	[Route("/hoursmanagement/roomentry")]
	//[RequiredPermission(ApplyTo.Get, "RoomHoursEntry_Get")]
	//[RequiredPermission(ApplyTo.Post, "RoomHoursEntry_Post")]
	//[RequiredPermission(ApplyTo.Put, "RoomHoursEntry_Put")]
	//[RequiredPermission(ApplyTo.Delete, "RoomHoursEntry_Delete")]
	public class RoomHoursEntry:IReturn<RoomHoursEntry>
	{
		public long Id { get; set; }
		public long HourTypeId { get; set; }
		public HourType HourType { get; set; }
		public long CostCentreId { get; set; }
		public CostCentre CostCentre { get; set; }
		public int ConceptNumber { get; set; }
		public int OrderNumber { get; set; }
		public int RoomNumber { get; set; }
		public int PersonNumber { get; set; }
		public decimal Hours { get; set; }
		public string Description { get; set; }
		public DateTime DeliveryDate { get; set; }
		public bool Deleted { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}