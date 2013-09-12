using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices
{
	public class HelloService : Service
	{
		public object Any(Hello request)
		{
			return new HelloResponse { Result = "Hello, " + request.Name };
		}
	}
}