﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[RequiredPermission(ApplyTo.Get, "LevelLists_Get")]
	[RequiredPermission(ApplyTo.Put, "LevelLists_Put")]
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