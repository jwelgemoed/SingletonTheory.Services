using System.Collections.Generic;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[RequiredPermission(ApplyTo.Get, "DomainPermissionFunctionalPermissions_Get")]
	[RequiredPermission(ApplyTo.Put, "DomainPermissionFunctionalPermissions_Put")]
	[Route("/auth/admin/domainpermission/{DomainPermissionId}/functionalpermissions")]
	public class DomainPermissionFunctionalPermissions : IReturn<DomainPermissionFunctionalPermissions>
	{
		public DomainPermissionFunctionalPermissions()
		{
			Assigned = new List<FunctionalPermission>();
			UnAssigned = new List<FunctionalPermission>();
		}

		public int DomainPermissionId { get; set; }
		public List<FunctionalPermission> Assigned { get; set; }
		public List<FunctionalPermission> UnAssigned { get; set; }
	}
}