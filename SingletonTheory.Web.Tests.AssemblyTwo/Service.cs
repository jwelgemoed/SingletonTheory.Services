using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonTheory.Web.Tests.AssemblyTwo
{
	public class Service : ServiceStack.ServiceInterface.Service
	{
		public Response Get(RequestTwo request)
		{
			return new Response() { SomeTextField = "Hello World from AssemblyTwo.Service", AssemblyNumber = 2 };
		}
	}
}
