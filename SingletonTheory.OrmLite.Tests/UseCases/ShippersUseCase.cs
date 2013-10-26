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

				var trainsAreUs = db.Select<Shipper>(x => x.ShipperTypeId == trainsType.Id).First<Shipper>();
				Assert.That(trainsAreUs.CompanyName, Is.EqualTo("Trains R Us"));
				Assert.That(db.Select<Shipper>(x => x.CompanyName == "Trains R Us" || x.Phone == "555-UNICORNS"), Has.Count.EqualTo(2));
				Assert.That(db.Select<Shipper>(x => x.ShipperTypeId == planesType.Id), Has.Count.EqualTo(2));

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