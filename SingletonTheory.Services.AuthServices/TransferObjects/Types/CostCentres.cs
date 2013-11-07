using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/hoursmanagement/costcentres")]
	public class CostCentres:IReturn<List<CostCentre>>
	{
	}
}