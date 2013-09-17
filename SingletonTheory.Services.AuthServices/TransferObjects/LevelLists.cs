using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/role/{RoleId}/domainpermissions")]
	[Route("/auth/admin/domainpermission/{DomainPermissionId}/functionalpermissions")]
	[Route("/auth/admin/functionalpermission/{FunctionalPermissionId}/permissions")]
	public class LevelLists : IReturn<LevelLists>
	{
		public LevelLists()
		{
			Assigned = new List<object>();
			UnAssigned = new List<object>();
		}

		public int RoleId { get; set; }
		public int DomainPermissionId { get; set; }
		public int FunctionalPermissionId { get; set; }
		public List<object> Assigned { get; set; }
		public List<object> UnAssigned { get; set; } 
	}
}