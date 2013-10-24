using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects.Localization;

namespace SingletonTheory.Services.AuthServices.Tests.Services
{
	[TestFixture]
	public class LocalizationServiceTests
	{
		#region Fields & Properties

		private JsonServiceClient _client;

		#endregion Fields & Properties

		#region Setup & Teardown

		[SetUp]
		public void SetUp()
		{
			_client = HTTPClientHelpers.GetClient(HTTPClientHelpers.RootUrl, HTTPClientHelpers.UserName, HTTPClientHelpers.Password);
		}

		[TearDownAttribute]
		public void TearDown()
		{
			_client.Dispose();
			_client = null;
		}

		#endregion Setup & Teardown

		#region Test Methods

		[Test]
		public void ShouldReturnLocale()
		{
			// Arrange
			LocalizationDictionary request = new LocalizationDictionary();
			request.Locale = "nl-nl";

			// Act
			LocalizationDictionary response = _client.Get<LocalizationDictionary>(request);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.Locale, Is.EqualTo(request.Locale), "Returns invalid locale");
		}

		[Test]
		public void ShouldReturnCorrectValueForLocale()
		{
			// Arrange
			LocalizationDictionary request = new LocalizationDictionary();
			request.LocalizationData.Add(new LocalizationItem() { Key = "_MainTitle_" });
			request.Locale = "nl-nl";

			// Act
			LocalizationDictionary response = _client.Get<LocalizationDictionary>(request);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.LocalizationData.Count, Is.EqualTo(1));
			Assert.That(response.LocalizationData[0].Value, Is.EqualTo("Singleton Theory Toegangsapplicatie."), "Does not returned correct value");
		}

		[Test]
		public void ShouldReturnAllValuesForLocale()
		{
			// Arrange
			LocalizationDictionary request = new LocalizationDictionary();
			request.Locale = "nl-nl";

			// Act
			LocalizationDictionary response = _client.Get<LocalizationDictionary>(request);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.LocalizationData.Count, Is.EqualTo(31));
		}

		#endregion Test Methods
	}
}
