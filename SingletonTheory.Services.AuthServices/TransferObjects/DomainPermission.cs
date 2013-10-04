using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/domainpermission")]
	[RequiredPermission(ApplyTo.Get, "DomainPermission_Get")]
	[RequiredPermission(ApplyTo.Put, "DomainPermission_Put")]
	[RequiredPermission(ApplyTo.Post, "DomainPermission_Post")]
	[RequiredPermission(ApplyTo.Delete, "DomainPermission_Delete")]
	public class DomainPermission : IReturn<DomainPermission>
	{
		public int Id { get; set; }
		public string Label { get; set; }
		public string Description { get; set; }
		public int[] FunctionalPermissionIds { get; set; }
	}
}