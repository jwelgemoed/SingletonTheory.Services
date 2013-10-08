using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[Route("/auth/admin/domainpermissions")]
	[RequiredPermission(ApplyTo.Get, "DomainPermissions_Get")]
	public class DomainPermissions : IReturn<List<DomainPermission>>
	{
	}
}