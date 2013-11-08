using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Hours
{
	//[RequiredPermission(ApplyTo.Get, "RoomHoursEntries_Get")]
	//[RequiredPermission(ApplyTo.Post, "RoomHoursEntries_Post")]
	//[RequiredPermission(ApplyTo.Put, "RoomHoursEntries_Put")]
	//[RequiredPermission(ApplyTo.Delete, "RoomHoursEntries_Delete")]
	public class RoomHoursEntries:IReturn<List<RoomHoursEntry>>
	{
	}
}