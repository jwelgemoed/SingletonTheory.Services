using NUnit.Framework;
using ServiceStack.ServiceClient.Web;

namespace SingletonTheory.Services.AuthServices.Tests
{
	[TestFixture]
	public class HelloServiceTests
	{
		[Test]
		public void ShouldReturnHelloMax()
		{
			// Arrange
			JsonServiceClient client = new JsonServiceClient("http://localhost:54720/");
			string expected = "Hello, Max";

			// Act
			HelloResponse response = client.Send<HelloResponse>(new Hello { Name = "Max" });

			// Assert
			Assert.AreEqual(expected, response.Result);
		}
	}
}
