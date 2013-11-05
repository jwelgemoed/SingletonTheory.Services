using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	public class CostCentre:IReturn<CostCentre>
	{
		public long Id { get; set; }
		public string LookupCode { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}