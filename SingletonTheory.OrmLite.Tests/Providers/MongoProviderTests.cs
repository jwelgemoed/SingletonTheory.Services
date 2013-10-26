using NUnit.Framework;
using SingletonTheory.OrmLite.Tests.Config;
using SingletonTheory.OrmLite.Tests.Data;
using System;
using System.Linq;

namespace SingletonTheory.OrmLite.Providers
{
	[TestFixture]
	public class MongoProviderTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionForNullConnectionString()
		{
			// Act
			try
			{
				using (MongoProvider provider = new MongoProvider(null, typeof(Shipper)))
				{
					// Assert
					Assert.Fail("Should not allow empty connection strings");
				}
			}
			catch (ArgumentNullException)
			{
				Assert.Pass();
			}
		}

		[Test]
		public void ShouldThrowArgumentNullExceptionForNullModelType()
		{
			// Act
			try
			{
				using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, null))
				{
					// Assert
					Assert.Fail("Should not allow null modelType");
				}
			}
			catch (ArgumentNullException)
			{
				Assert.Pass();
			}
		}

		[Test]
		public void ShouldHaveSetDialectProvider()
		{
			// Act
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				// Assert
				//Assert.IsInstanceOf<MongoDialectProvider>(OrmLiteConfig.DialectProvider);
			}
		}

		[Test]
		public void ShouldDropAndCreate()
		{
			// Arrange
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				// Act
				provider.DropAndCreate(typeof(Shipper));

				// Assert
				Assert.IsTrue(provider.TableExists(typeof(Shipper)));
			}
		}

		[Test]
		public void ShouldClearTables()
		{
			// Arrange
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);
				shipper = provider.Insert<Shipper>(shipper);

				// Act
				provider.DeleteAll<Shipper>();

				// Assert
				Assert.AreEqual(provider.Select<Shipper>().Count, 0);
				Assert.AreEqual(provider.Select<ShipperContact>().Count, 0);
			}
		}

		[Test]
		public void ShouldClearLookupTables()
		{
			// Arrange
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);

				// Act
				provider.DeleteAll<ShipperType>();

				// Assert
				Assert.AreEqual(provider.Select<ShipperType>().Count, 0);
			}
		}

		[Test]
		public void ShouldNotClearLookupTables()
		{
			// Arrange
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);

				// Act
				provider.DeleteAll<Shipper>();

				// Assert
				Assert.AreEqual(provider.Select<ShipperType>().Count, 1);
			}
		}

		[Test]
		public void ShouldInsertShipper()
		{
			// Arrange
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);

				// Act
				shipper = provider.Insert<Shipper>(shipper);

				// Assert
				Assert.IsNotNull(provider.SelectById<Shipper>(shipper.Id));
			}
		}

		[Test]
		public void ShouldSelectShipperAndTree()
		{
			// Arrange
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);
				shipper = provider.Insert<Shipper>(shipper);

				// Act
				Shipper savedTree = provider.SelectById<Shipper>(shipper.Id);

				// Assert
				Assert.IsNotNull(savedTree);
				Assert.AreEqual(savedTree.ShipperContacts.Count, 1);
				Assert.IsNotNull(savedTree.ShipperContacts[0]);
			}
		}

		[Test]
		public void ShouldSelectShipperAndTreeWithExpression()
		{
			// Arrange
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);
				shipper = provider.Insert<Shipper>(shipper);

				// Act
				Shipper savedTree = provider.Select<Shipper>(x => x.Id == shipper.Id).First();

				// Assert
				Assert.IsNotNull(savedTree);
				Assert.AreEqual(savedTree.ShipperContacts.Count, 1);
				Assert.IsNotNull(savedTree.ShipperContacts[0]);
			}
		}

		[Test]
		public void ShouldUpdateShipper()
		{
			// Arrange
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);
				provider.Insert<Shipper>(shipper);
				shipper.CompanyName += " Changed";
				shipper.ShipperContacts[0].EmailAddress += shipper.ShipperContacts[0].EmailAddress + " Changed";

				// Act
				provider.Update<Shipper>(shipper);

				// Assert
				Shipper updatedShipper = provider.SelectById<Shipper>(shipper.Id);
				Assert.AreEqual(shipper.CompanyName, updatedShipper.CompanyName);
				Assert.AreEqual(shipper.ShipperContacts[0].EmailAddress, updatedShipper.ShipperContacts[0].EmailAddress);
			}
		}

		[Test]
		public void ShouldDeleteShipper()
		{
			// Arrange
			using (MongoProvider provider = new MongoProvider(ConfigSettings.MongoConnectionString, typeof(Shipper)))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);
				provider.Insert<Shipper>(shipper);

				// Act
				provider.Delete<Shipper>(shipper);

				// Assert
				Assert.IsNull(provider.SelectById<Shipper>(shipper.Id));
				Assert.IsNull(provider.SelectById<Shipper>(shipper.ShipperContacts[0].Id));
			}
		}

		#endregion Test Methods
	}
}