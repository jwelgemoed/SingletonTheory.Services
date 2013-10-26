using NUnit.Framework;
using SingletonTheory.OrmLite.Providers;
using SingletonTheory.OrmLite.Tests.Config;
using SingletonTheory.OrmLite.Tests.Data;
using System.Linq;

namespace ServiceStack.OrmLite.Tests
{
	[TestFixture]
	public class ShippersUseCase
	{
		#region Test Methods

		[Test]
		public void Shippers_UseCase()
		{
			ShipperType trainsType, planesType;

			using (SqlProvider db = new SqlProvider(ConfigSettings.SqlConnectionString, typeof(Shipper), false))
			{
				DataProvider.DropAndCreate(db);
			}

			using (SqlProvider db = new SqlProvider(ConfigSettings.SqlConnectionString, typeof(Shipper), true))
			{
				// Playing with transactions
				trainsType = db.Insert(new ShipperType { Name = "Trains" });
				planesType = db.Insert(new ShipperType { Name = "Planes" });
			}

			using (SqlProvider db = new SqlProvider(ConfigSettings.SqlConnectionString, typeof(Shipper), true))
			{
				db.Insert(new ShipperType { Name = "Automobiles" });
				Assert.That(db.Select<ShipperType>(), Has.Count.EqualTo(3));

				db.Rollback();
			}

			using (SqlProvider db = new SqlProvider(ConfigSettings.SqlConnectionString, typeof(Shipper), true))
			{
				Assert.That(db.Select<ShipperType>(), Has.Count.EqualTo(2));

				// Performing standard Insert's and Selects
				db.Insert(GetTrainsRUsShipper(trainsType));
				db.Insert(GetPlanesRUsShipper(planesType));
				db.Insert(GetWeDoEverythingShipper(planesType));

				var trainsAreUs = db.Select<Shipper>("ShipperTypeId = {0}", trainsType.Id).First<Shipper>();
				Assert.That(trainsAreUs.CompanyName, Is.EqualTo("Trains R Us"));
				Assert.That(db.Select<Shipper>("CompanyName = {0} OR Phone = {1}", "Trains R Us", "555-UNICORNS"), Has.Count.EqualTo(2));
				Assert.That(db.Select<Shipper>("ShipperTypeId = {0}", planesType.Id), Has.Count.EqualTo(2));

				// Lets update a record
				trainsAreUs.Phone = "666-TRAINS";
				db.Update(trainsAreUs);
				Shipper shipper = db.SelectById<Shipper>(trainsAreUs.Id);
				Assert.That(shipper.Phone, Is.EqualTo("666-TRAINS"));

				// Then make it dissappear
				db.Delete<Shipper>(trainsAreUs);

				Shipper deletedItem = db.SelectById<Shipper>(trainsAreUs.Id) as Shipper;
				Assert.IsNull(deletedItem);

				// And bring it back again
				db.Insert(trainsAreUs);

				// Performing custom queries
				// Select only a subset from the table
				var partialColumns = db.Select<SubsetOfShipper>(typeof(Shipper), "ShipperTypeId = {0}", planesType.Id);
				Assert.That(partialColumns, Has.Count.EqualTo(2));

				// Select into another POCO class that matches sql
				var rows = db.Select<ShipperTypeCount>("SELECT ShipperTypeId, COUNT(*) AS Total FROM Shipper GROUP BY ShipperTypeId ORDER BY COUNT(*)");

				Assert.That(rows, Has.Count.EqualTo(2));
				Assert.That(rows[0].ShipperTypeId, Is.EqualTo(trainsType.Id));
				Assert.That(rows[0].Total, Is.EqualTo(1));
				Assert.That(rows[1].ShipperTypeId, Is.EqualTo(planesType.Id));
				Assert.That(rows[1].Total, Is.EqualTo(2));

				// And finally lets quickly clean up the mess we've made:
				db.DeleteAll<Shipper>();
				db.DeleteAll<ShipperType>();

				Assert.That(db.Select<Shipper>(), Has.Count.EqualTo(0));
				Assert.That(db.Select<ShipperType>(), Has.Count.EqualTo(0));
			}
		}

		#endregion Test Methods

		#region Helper Methods

		private static Shipper GetWeDoEverythingShipper(ShipperType planesType)
		{
			return GetShipper("We do everything!", "555-UNICORNS", planesType.Id);
		}

		private static Shipper GetPlanesRUsShipper(ShipperType planesType)
		{
			return GetShipper("Planes R Us", "555-PLANES", planesType.Id);
		}

		private static Shipper GetTrainsRUsShipper(ShipperType trainsType)
		{
			return GetShipper("Trains R Us", "555-TRAINS", trainsType.Id);
		}

		private static Shipper GetShipper(string companyName, string phone, long shipperTypeId)
		{
			return new Shipper { Id = 0, CompanyName = companyName, Phone = phone, ShipperTypeId = shipperTypeId };
		}

		#endregion Helper Methods
	}
}