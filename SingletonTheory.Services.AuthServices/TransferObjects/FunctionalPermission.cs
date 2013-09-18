using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using SingletonTheory.Services.AuthServices.Interfaces;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/functionalpermission")]
	public class FunctionalPermission : INameLabel, IReturn<FunctionalPermission>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Label { get; set; }
		public int[] PermissionIds { get; set; }
	}
}