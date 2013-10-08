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
			Assigned = new List<object>();
			UnAssigned = new List<object>();
		}

		public int FunctionalPermissionId { get; set; }
		public List<object> Assigned { get; set; }
		public List<object> UnAssigned { get; set; }
	}
}