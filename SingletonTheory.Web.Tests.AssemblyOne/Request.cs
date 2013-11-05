using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Web.Tests.AssemblyOne
{
	public class RequestOne : IReturn<Response>
	{
		public int Id { get; set; }
	}
}
