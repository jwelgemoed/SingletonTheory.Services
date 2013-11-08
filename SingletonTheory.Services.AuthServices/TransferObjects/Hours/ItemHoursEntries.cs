using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Hours
{
	[Route("/hoursmanagement/itementries")]
	//[RequiredPermission(ApplyTo.Get, "ItemHoursEntries_Get")]
	//[RequiredPermission(ApplyTo.Post, "ItemHoursEntries_Post")]
	//[RequiredPermission(ApplyTo.Put, "ItemHoursEntries_Put")]
	//[RequiredPermission(ApplyTo.Delete, "ItemHoursEntries_Delete")]
	public class ItemHoursEntries:IReturn<List<ItemHoursEntry>>
	{
	}
}