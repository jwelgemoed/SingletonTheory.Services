using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[RequiredPermission(ApplyTo.Get, "FunctionalPermissionPermissions_Get")]
	[RequiredPermission(ApplyTo.Put, "FunctionalPermissionPermissions_Put")]
	[Route("/auth/admin/functionalpermission/{FunctionalPermissionId}/permissions")]
	public class FunctionalPermissionPermissions : IReturn<FunctionalPermissionPermissions>
	{
		public FunctionalPermissionPermissions()
		{
			Assigned = new List<Permission>();
			UnAssigned = new List<Permission>();
		}

		public int FunctionalPermissionId { get; set; }
		public List<Permission> Assigned { get; set; }
		public List<Permission> UnAssigned { get; set; }
	}
}