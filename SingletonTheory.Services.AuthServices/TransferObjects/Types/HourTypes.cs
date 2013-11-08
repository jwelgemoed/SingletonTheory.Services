using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/hoursmanagement/hourtypes")]
	//[RequiredPermission(ApplyTo.Get, "HourTypes_Get")]
	//[RequiredPermission(ApplyTo.Post, "HourTypes_Post")]
	//[RequiredPermission(ApplyTo.Put, "HourTypes_Put")]
	//[RequiredPermission(ApplyTo.Delete, "HourTypes_Delete")]
	public class HourTypes:IReturn<List<HourType>>
	{
	}
}