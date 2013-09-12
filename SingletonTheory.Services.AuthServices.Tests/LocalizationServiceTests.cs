using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Tests
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
			LocalizationDictionaryRequest request = new LocalizationDictionaryRequest();
			request.Locale = "nl-nl";

			// Act
			LocalizationDictionaryResponse response = _client.Get<LocalizationDictionaryResponse>(request);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.Locale, Is.EqualTo(request.Locale), "Returns invalid locale");
		}

		[Test]
		public void ShouldReturnCorrectValueForLocale()
		{
			// Arrange
			LocalizationDictionaryRequest request = new LocalizationDictionaryRequest();
			request.LocalizationDictionary.Add(new LocalizationItem() { Key = "_MainTitle_" });
			request.Locale = "nl-nl";

			// Act
			LocalizationDictionaryResponse response = _client.Get<LocalizationDictionaryResponse>(request);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.LocalizationItems.Count, Is.EqualTo(1));
			Assert.That(response.LocalizationItems[0].Value, Is.EqualTo("Singleton Theory Toegangsapplicatie."), "Does not returned correct value");
		}

		[Test]
		public void ShouldReturnAllValuesForLocale()
		{
			// Arrange
			LocalizationDictionaryRequest request = new LocalizationDictionaryRequest();
			request.Locale = "nl-nl";

			// Act
			LocalizationDictionaryResponse response = _client.Get<LocalizationDictionaryResponse>(request);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.LocalizationItems.Count, Is.EqualTo(31));
		}

		#endregion Test Methods
	}
}
