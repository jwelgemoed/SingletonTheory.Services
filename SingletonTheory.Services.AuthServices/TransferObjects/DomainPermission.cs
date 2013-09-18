using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/domainpermission")]
	public class DomainPermission : IReturn<DomainPermission>
	{
		public int Id { get; set; }
		public string Label { get; set; }
		public string Description { get; set; }
		public int[] FunctionalPermissionIds { get; set; }
	}
}