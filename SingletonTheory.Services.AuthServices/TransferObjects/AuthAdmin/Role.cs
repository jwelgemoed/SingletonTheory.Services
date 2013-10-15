using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[Route("/auth/admin/role")]
	[RequiredPermission(ApplyTo.Get, "Role_Get")]
	[RequiredPermission(ApplyTo.Put, "Role_Put")]
	[RequiredPermission(ApplyTo.Post, "Role_Post")]
	[RequiredPermission(ApplyTo.Delete, "Role_Delete")]
	public class Role : IReturn<Role>
	{
		public int RootParentId { get; set; }
		public int ParentId { get; set; }
		public int Id { get; set; }
		public string Label { get; set; }
		public string Description { get; set; }
		public int[] DomainPermissionIds { get; set; }
	}
}