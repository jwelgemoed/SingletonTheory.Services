using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices
{
	[Route("/hello")]
	[Route("/hello/{Name}")]
	public class Hello
	{
		public string Name { get; set; }
	}
}