using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/types/occupationname")]
	//[RequiredPermission(ApplyTo.Get, "OccupationName_Get")]
	public class OccupationName
	{
		public long Id { get; set; }
		public string Description { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}