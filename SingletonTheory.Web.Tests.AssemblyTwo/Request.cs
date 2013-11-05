using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonTheory.Web.Tests.AssemblyTwo
{
	public class RequestTwo : IReturn<Response>
	{
		public int Id { get; set; }
	}
}
