using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[Route("/auth/admin/roletree")]
	[Route("/auth/admin/roletree/rootparentid/{RootParentId}")]
	[RequiredPermission(ApplyTo.Get, "Role_Get")]
	public class RoleTree : IReturn<RoleTree>
	{
		public int RootParentId { get; set; }
		public List<TreeItem> TreeItems { get; set; }

		public RoleTree()
		{
			TreeItems = new List<TreeItem>();
		}
	}

	public class TreeItem
	{
		public int Id { get; set; }
		public string Label { get; set; }
		public List<TreeItem> Children { get; set; }
		public int RootParentId { get; set; }
		public int ParentId { get; set; }
	}
}