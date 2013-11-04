using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonTheory.Web.Tests.AssemblyOne
{
	public class Service : ServiceStack.ServiceInterface.Service
	{
		public Response Get(RequestOne request)
		{
			return new Response() { SomeTextField = "Hello World from AssemblyOne.Service", AssemblyNumber = 1 };
		}
	}
}
