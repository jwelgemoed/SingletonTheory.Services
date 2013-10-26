using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[Route("/auth/admin/rolesrolecanmoveto")]
	[Route("/auth/admin/rolesrolecanmoveto/id/{Id}")]
	[RequiredPermission(ApplyTo.Get, "Role_Get")]
	public class RolesRoleCanMoveTo : IReturn<List<Role>>
	{
		public int Id { get; set; }
	}
}