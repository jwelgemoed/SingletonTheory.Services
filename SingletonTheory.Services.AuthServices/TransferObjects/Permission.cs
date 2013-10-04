using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Interfaces;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/permission")]
	[RequiredPermission(ApplyTo.Get, "Permission_Get")]
	[RequiredPermission(ApplyTo.Put, "Permission_Put")]
	[RequiredPermission(ApplyTo.Post, "Permission_Post")]
	public class Permission : INameLabel, IReturn<Permission>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Label { get; set; }
	}
}