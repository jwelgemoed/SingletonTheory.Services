using NUnit.Framework;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.OrmLite.Providers;
using SingletonTheory.OrmLite.Tests.Config;
using SingletonTheory.OrmLite.Tests.Data;
using SingletonTheory.OrmLite.Tests.Repositories;

namespace ServiceStack.OrmLite.Tests
{
	[TestFixture]
	public class SqlProvidersTests
	{
		#region Fields & Properties

		ShippersRepositorySample _repository;

		#endregion Fields & Properties

		#region Setup & Teardown

		[SetUp]
		public void SetUp()
		{
			_repository = new ShippersRepositorySample();
			TearDown();
		}

		[TearDown]
		public void TearDown()
		{
			using (IDatabaseProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper), false))
			{
				_repository.DropAndCreate(provider);
			}
		}

		#endregion Setup & Teardown

		#region Test Methods

		[Test]
		public void ShouldCreateInstanceOfTypeShipperWithIntKey()
		{
			// Arrange
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper), false))
			{
				// Act
				Shipper shipper = _repository.CreateInstance<Shipper>(provider);

				// Assert
				Assert.IsNotNull(shipper);
				Assert.IsInstanceOf<Shipper>(shipper);
			}
		}

		[Test]
		public void ShouldClearCollections()
		{
			// Arrange
			using (SqlProvider provider = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper), false))
			{
				// Act
				_repository.DropAndCreate(provider);

				// Assert
				Assert.IsTrue(provider.TableExists(typeof(Shipper)));
				Assert.IsTrue(provider.TableExists(typeof(ShipperType)));
				Assert.IsTrue(provider.TableExists(typeof(ShipperContact)));
			}
		}

		[Test]
		public void Shippers_UseCase()
		{
			ShipperType trainsType, planesType;

			using (SqlProvider db = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper), true))
			{
				// Playing with transactions
				trainsType = db.Insert(new ShipperType { Name = "Trains" });
				planesType = db.Insert(new ShipperType { Name = "Planes" });
			}

			using (SqlProvider db = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper), true))
			{
				db.Insert(new ShipperType { Name = "Automobiles" });
				Assert.That(db.Select<ShipperType>(), Has.Count.EqualTo(3));

				db.Rollback();
			}

			using (SqlProvider db = new SqlProvider(ConfigSettings.ConnectionString, typeof(Shipper), true))
			{
				Assert.That(db.Select<ShipperType>(), Has.Count.EqualTo(2));

				// Performing standard Insert's and Selects
				//db.Insert(new Shipper { Id = 0, CompanyName = "Trains R Us", Phone = "555-TRAINS", ShipperTypeId = trainsTypeId });
				//db.Insert(new Shipper { CompanyName = "Planes R Us", Phone = "555-PLANES", ShipperTypeId = planesTypeId });
				//db.Insert(new Shipper { CompanyName = "We do everything!", Phone = "555-UNICORNS", ShipperTypeId = planesTypeId });

				//var trainsAreUs = db.Single<Shipper>("ShipperTypeId = {0}", trainsTypeId);
				//Assert.That(trainsAreUs.CompanyName, Is.EqualTo("Trains R Us"));
				//Assert.That(db.Select<Shipper>("CompanyName = {0} OR Phone = {1}", "Trains R Us", "555-UNICORNS"), Has.Count.EqualTo(2));
				//Assert.That(db.Select<Shipper>("ShipperTypeId = {0}", planesTypeId), Has.Count.EqualTo(2));

				// Lets update a record
				//trainsAreUs.Phone = "666-TRAINS";
				//db.Update(trainsAreUs);
				//Shipper shipper = db.SelectById<Shipper>(trainsAreUs.Id) as Shipper;
				//Assert.That(shipper.Phone, Is.EqualTo("666-TRAINS"));

				// Then make it dissappear
				//db.Delete<Shipper>(trainsAreUs);

				//Shipper deletedItem = db.SelectById<Shipper>(trainsAreUs.Id) as Shipper;
				//Assert.IsNull(deletedItem);

				// And bring it back again
				//db.Insert(trainsAreUs);

				// Performing custom queries
				// Select only a subset from the table
				//var partialColumns = db.Select<SubsetOfShipper>(typeof(Shipper), "ShipperTypeId = {0}", planesTypeId);
				//Assert.That(partialColumns, Has.Count.EqualTo(2));

				// Select into another POCO class that matches sql
				var rows = db.Select<ShipperTypeCount>("SELECT ShipperTypeId, COUNT(*) AS Total FROM Shippers GROUP BY ShipperTypeId ORDER BY COUNT(*)");

				Assert.That(rows, Has.Count.EqualTo(2));
				Assert.That(rows[0].ShipperTypeId, Is.EqualTo(trainsType));
				Assert.That(rows[0].Total, Is.EqualTo(1));
				Assert.That(rows[1].ShipperTypeId, Is.EqualTo(planesType));
				Assert.That(rows[1].Total, Is.EqualTo(2));

				// And finally lets quickly clean up the mess we've made:
				//db.DeleteAll<Shipper>();
				db.DeleteAll<ShipperType>();

				//Assert.That(db.Select<Shipper>(), Has.Count.EqualTo(0));
				Assert.That(db.Select<ShipperType>(), Has.Count.EqualTo(0));
			}
		}

		#endregion Test Methods
	}
}