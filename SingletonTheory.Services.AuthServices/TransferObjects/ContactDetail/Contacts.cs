using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail
{
	[Route("/contacts")]
	[RequiredPermission(ApplyTo.Get, "Contacts_Get")]
	public class Contacts : IReturn<List<Contacts>>
	{
	}
}