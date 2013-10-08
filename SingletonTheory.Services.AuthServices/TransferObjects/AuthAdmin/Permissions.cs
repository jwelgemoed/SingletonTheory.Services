using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[Route("/auth/admin/permissions")]
	[RequiredPermission(ApplyTo.Get, "Permissions_Get")]
	public class Permissions : IReturn<List<Permission>>
	{
	}
}