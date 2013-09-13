using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/role")]
	public class Role : IReturn<List<Role>>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Label { get; set; }
		public int[] GroupLvl2Ids { get; set; }
	}
}