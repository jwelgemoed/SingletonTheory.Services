using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail
{
	[Route("/contactdetails/contactdetails")]
	//[RequiredPermission(ApplyTo.Get, "Contacts_Get")]
	public class ContactDetails : IReturn<List<ContactDetail>>
	{
	}
}