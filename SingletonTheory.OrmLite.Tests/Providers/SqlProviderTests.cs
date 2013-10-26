using NUnit.Framework;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using SingletonTheory.OrmLite.Tests.Config;
using SingletonTheory.OrmLite.Tests.Data;
using System;

namespace SingletonTheory.OrmLite.Providers
{
	[TestFixture]
	public class SqlProviderTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionForNullConnectionString()
		{
			// Act
			try
			{
				using (SqlProvider provider = new SqlProvider(null, typeof(Shipper)))
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
				using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, null))
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
		public void ShouldHaveSetDialectProviderToSqlServerOrmLiteDialectProvider()
		{
			// Act
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper)))
			{
				// Assert
				Assert.IsInstanceOf<SqlServerOrmLiteDialectProvider>(OrmLiteConfig.DialectProvider);
			}
		}

		[Test]
		public void ShouldDropAndCreateTables()
		{
			// Arrange
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper)))
			{
				// Act
				provider.DropAndCreate(typeof(Shipper));

				// Assert
				Assert.IsTrue(provider.TableExists(typeof(Shipper)));
				Assert.IsTrue(provider.TableExists(typeof(ShipperContact)));
			}
		}

		[Test]
		public void ShouldClearTables()
		{
			// Arrange
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper)))
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
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper)))
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
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper)))
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
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper)))
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
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper)))
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
		public void ShouldUpdateShipper()
		{
			// Arrange
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper)))
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
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper)))
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
