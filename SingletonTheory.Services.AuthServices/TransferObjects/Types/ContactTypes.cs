using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/contacttypes")]
	//[RequiredPermission(ApplyTo.Get, "ContactTypes_Get")]
	public class ContactTypes : IReturn<List<ContactType>>
	{
	}
}