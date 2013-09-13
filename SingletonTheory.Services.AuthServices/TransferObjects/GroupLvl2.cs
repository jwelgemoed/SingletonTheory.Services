using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/admin/groupLvl2")]
	public class GroupLvl2 : IReturn<List<GroupLvl2>>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Label { get; set; }
		public int[] GroupLvl1Ids { get; set; }
	}
}