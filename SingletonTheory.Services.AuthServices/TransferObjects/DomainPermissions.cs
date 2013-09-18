using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/domainpermissions")]
	public class DomainPermissions : IReturn<List<DomainPermission>>
	{
	}
}