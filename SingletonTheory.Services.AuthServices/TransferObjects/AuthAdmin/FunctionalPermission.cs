using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using SingletonTheory.Services.AuthServices.Interfaces;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[Route("/auth/admin/functionalpermission")]
	[RequiredPermission(ApplyTo.Get, "FunctionalPermission_Get")]
	[RequiredPermission(ApplyTo.Put, "FunctionalPermission_Put")]
	[RequiredPermission(ApplyTo.Post, "FunctionalPermission_Post")]
	[RequiredPermission(ApplyTo.Delete, "FunctionalPermission_Delete")]
	public class FunctionalPermission : INameLabel, IReturn<FunctionalPermission>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Label { get; set; }
		public List<int> PermissionIds { get; set; }
	}
}