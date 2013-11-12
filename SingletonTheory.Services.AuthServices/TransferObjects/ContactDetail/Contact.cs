using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail
{
	[Route("/contactdetails/contact")]
	[Route("/contactdetails/contact/id/{Id}")]
	public class Contact
	{
		//Contact
		public long Id { get; set; }
		public long EntityId { get; set; }
		public long ContactTypeId { get; set; }
		public string Value { get; set; }
		public bool Preferred { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}