using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail
{
	[Route("/contactdetails/contacts")]
	[Route("/contactdetails/contacts/id/{Id}")]
	public class Contacts : IReturn<List<Contact>>
	{
	}
}