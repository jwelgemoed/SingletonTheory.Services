using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[RequiredPermission(ApplyTo.Get, "RoleDomainPermissions_Get")]
	[RequiredPermission(ApplyTo.Put, "RoleDomainPermissions_Put")]
	[Route("/auth/admin/role/{RoleId}/domainpermissions")]
	public class RoleDomainPermissions : IReturn<RoleDomainPermissions>
	{
		public RoleDomainPermissions()
		{
			Assigned = new List<DomainPermission>();
			UnAssigned = new List<DomainPermission>();
		}

		public int RoleId { get; set; }
		public List<DomainPermission> Assigned { get; set; }
		public List<DomainPermission> UnAssigned { get; set; }
	}
}