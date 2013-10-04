using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/roles")]
	[RequiredPermission(ApplyTo.Get, "Roles_Get")]
	public class Roles : IReturn<List<Role>>
	{
	}
}