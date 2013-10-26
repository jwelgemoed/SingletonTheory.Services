using NUnit.Framework;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using SingletonTheory.OrmLite.Config;
using SingletonTheory.OrmLite.Tests.Data;
using System;
using System.Collections.Generic;

namespace SingletonTheory.OrmLite.Tests.Config
{
	[TestFixture]
	public class ConfigExtensionsTests
	{
		#region Test Methods

		[Test]
		public void ShouldReturnNullForReferenceTypeInt()
		{
			// Act
			ModelDefinition modelDefinition = typeof(int).GetModelDefinition();

			// Assert
			Assert.IsNull(modelDefinition);
		}

		[Test]
		public void ShouldReturnNullForReferenceTypeString()
		{
			// Act
			ModelDefinition modelDefinition = typeof(string).GetModelDefinition();

			// Assert
			Assert.IsNull(modelDefinition);
		}

		[Test]
		public void ShouldThrowArgumentExceptionForListTypes()
		{
			// Act
			try
			{
				ModelDefinition modelDefinition = typeof(List<>).GetModelDefinition();

				// Assert
				Assert.Fail("Should not send framework types such as lists through to this method");
			}
			catch (ArgumentException)
			{
				// Assert
				Assert.Pass();
			}
		}

		[Test]
		public void ShouldBuildModelDefinitions()
		{
			// Act
			ModelDefinition modelDefinition = GetModeldefinition();

			// Assert
			Assert.IsNotNull(modelDefinition);
			Assert.AreEqual(modelDefinition.FieldDefinitions.Count, 4);
		}

		[Test]
		public void ShouldHaveSomeDerivedPropertyInIgnoredFieldDefinitions()
		{
			// Act
			ModelDefinition modelDefinition = GetModeldefinition();

			// Assert
			Assert.IsNotNull(modelDefinition);
			Assert.AreEqual(3, modelDefinition.IgnoredFieldDefinitions.Count);
		}

		[Test]
		public void ShouldHaveAliasAttributeSetToShipper()
		{
			// Act
			ModelDefinition modelDefinition = GetModeldefinition();

			// Assert
			Assert.IsNotNull(modelDefinition);
			Assert.AreEqual(modelDefinition.ModelName, "Shipper");
		}

		#endregion Test Methods

		#region Helper Methods

		private static ModelDefinition GetModeldefinition()
		{
			OrmLiteConfig.DialectProvider = SqlServerOrmLiteDialectProvider.Instance;
			ModelDefinition modelDefinition = typeof(Shipper).GetModelDefinition();
			return modelDefinition;
		}

		#endregion Helper Methods
	}
}
