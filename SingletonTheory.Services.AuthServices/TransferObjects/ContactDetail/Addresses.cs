using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail
{
	[Route("/contactdetails/addresses")]
	[Route("/contactdetails/addresses/id/{Id}")]
	public class Addresses
	{
		public long EntityId { get; set; }
	}
}