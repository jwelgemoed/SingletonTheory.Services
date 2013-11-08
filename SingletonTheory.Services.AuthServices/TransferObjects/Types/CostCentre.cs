using System;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	//[RequiredPermission(ApplyTo.Get, "CostCentre_Get")]
	//[RequiredPermission(ApplyTo.Post, "CostCentre_Post")]
	//[RequiredPermission(ApplyTo.Put, "CostCentre_Put")]
	//[RequiredPermission(ApplyTo.Delete, "CostCentre_Delete")] 
	public class CostCentre:IReturn<CostCentre>
	{
		public long Id { get; set; }
		public string LookupCode { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}