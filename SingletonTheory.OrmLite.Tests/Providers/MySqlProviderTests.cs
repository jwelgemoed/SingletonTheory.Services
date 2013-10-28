using NUnit.Framework;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.OrmLite.Tests.Config;
using SingletonTheory.OrmLite.Tests.Data;
using System;
using System.Linq;

namespace SingletonTheory.OrmLite.Providers
{
	[TestFixture]
	public class MySqlProviderTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionForNullConnectionString()
		{
			// Act
			try
			{
				using (MySqlProvider provider = new MySqlProvider(null))
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
		public void ShouldHaveSetDialectProvider()
		{
			// Act
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
			{
				// Assert
				Assert.IsInstanceOf<MySqlDialectProvider>(OrmLiteConfig.DialectProvider);
			}
		}

		[Test]
		public void ShouldCreateCollections()
		{
			// Arrange
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
			{
				// Act
				provider.CreateCollection(typeof(Shipper));

				// Assert
				Assert.IsTrue(provider.CollectionExists(typeof(Shipper)));
				Assert.IsTrue(provider.CollectionExists(typeof(ShipperContact)));
				Assert.IsTrue(provider.CollectionExists(typeof(ShipperType)));
			}
		}

		[Test]
		public void ShouldDropAndCreateTables()
		{
			// Arrange
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
			{
				// Act
				provider.DropAndCreate(typeof(Shipper));

				// Assert
				Assert.IsTrue(provider.CollectionExists(typeof(Shipper)));
				Assert.IsTrue(provider.CollectionExists(typeof(ShipperContact)));
			}
		}

		[Test]
		public void ShouldClearTables()
		{
			// Arrange
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
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
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
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
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
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
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);

				// Act
				shipper = provider.Insert<Shipper>(shipper);

				// Assert
				Assert.IsNotNull(provider.SelectById<Shipper>(shipper.Id));
				Assert.IsNotNull(provider.SelectById<ShipperContact>(shipper.ShipperContacts[0].Id));
			}
		}

		[Test]
		public void ShouldSelectShipperAndTree()
		{
			// Arrange
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
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
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
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
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
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
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
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

		[Test]
		public void ShouldDeleteAll()
		{
			// Arrange
			using (MySqlProvider provider = new MySqlProvider(ConfigSettings.MySqlConnectionString))
			{
				Shipper shipper = DataProvider.PreInsertArrange(provider);
				provider.Insert<Shipper>(shipper);

				// Act
				provider.DeleteAll<Shipper>();

				// Assert
				Assert.IsNull(provider.SelectById<Shipper>(shipper.Id));
				Assert.IsNull(provider.SelectById<Shipper>(shipper.ShipperContacts[0].Id));
			}
		}

		[Test]
		public void ShouldDropAndCreate()
		{
			using (IDatabaseProvider db = ProviderFactory.GetProvider(ConfigSettings.MySqlConnectionName, false))
			{
				DataProvider.DropAndCreate(db);
			}
		}

		#endregion Test Methods
	}
}
