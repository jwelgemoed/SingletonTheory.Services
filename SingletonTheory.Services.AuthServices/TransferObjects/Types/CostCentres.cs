using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/hoursmanagement/costcentres")]
	//[RequiredPermission(ApplyTo.Get, "CostCentres_Get")]
	//[RequiredPermission(ApplyTo.Post, "CostCentres_Post")]
	//[RequiredPermission(ApplyTo.Put, "CostCentres_Put")]
	//[RequiredPermission(ApplyTo.Delete, "CostCentres_Delete")]
	public class CostCentres:IReturn<List<CostCentre>>
	{
	}
}