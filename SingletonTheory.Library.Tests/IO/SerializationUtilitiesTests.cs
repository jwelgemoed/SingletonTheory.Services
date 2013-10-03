using NUnit.Framework;
using SingletonTheory.Library.IO;
using SingletonTheory.Library.Tests.TestObjects;

namespace SingletonTheory.Library.Tests.IO
{
	[TestFixture]
	public class SerializationUtilitiesTests
	{
		[Test]
		public void ShouldSerializeToJsonWithoutPrettyPrint()
		{
			// Arrange 
			MyTestObject objectToSerialize = new MyTestObject() { SomeValue = "Some value {0} to serialize" };

			// Act
			string serialized = SerializationUtilities.SerializeToJson(objectToSerialize, false);

			// Assert
			Assert.IsNotNullOrEmpty(serialized);
		}

		[Test]
		public void ShouldSerializeToJsonWithPrettyPrint()
		{
			// Arrange 
			MyTestObject objectToSerialize = new MyTestObject() { SomeValue = "Some value {0} to serialize" };

			// Act
			string serialized = SerializationUtilities.SerializeToJson(objectToSerialize, true);

			// Assert
			Assert.IsNotNullOrEmpty(serialized);
		}

		[Test]
		public void ShouldDeserializeToJsonWithoutPrettyPrint()
		{
			// Arrange
			string serialized = "{SomeValue:\"Some value {0} to serialize\"}";

			// Act
			MyTestObject deserializeObject = SerializationUtilities.DeserializeFromJson<MyTestObject>(serialized);

			// Assert
			Assert.IsNotNull(deserializeObject);
		}

		[Test]
		public void ShouldDeserializeToJsonWithPrettyPrint()
		{
			// Arrange
			string serialized = "{\r\n\tSomeValue: Some value {\r\n\t\t0\r\n\t} to serialize\r\n}";

			// Act
			MyTestObject deserializeObject = SerializationUtilities.DeserializeFromJson<MyTestObject>(serialized);

			// Assert
			Assert.IsNotNull(deserializeObject);
		}
	}
}
