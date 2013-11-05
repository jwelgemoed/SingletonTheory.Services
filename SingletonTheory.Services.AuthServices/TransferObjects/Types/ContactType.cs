using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/types/contacttype")]
	//[RequiredPermission(ApplyTo.Get, "ContactType_Get")]
	public class ContactType
	{
		public long Id { get; set; }
		public string Description { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}