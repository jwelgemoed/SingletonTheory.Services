using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/types/occupationnames")]
	//[RequiredPermission(ApplyTo.Get, "OccupationNames_Get")]
	public class OccupationNames : IReturn<List<OccupationName>>
	{
	}
}