using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/functionalpermissions")]
	[RequiredPermission(ApplyTo.Get, "CurrentUserAuthRequest_Get")]
	public class FunctionalPermissions : IReturn<List<FunctionalPermission>>
	{
	}
}