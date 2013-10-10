﻿using MultiDatabaseSupport.Data;
using MultiDatabaseSupport.Tests.Config;
using NUnit.Framework;
using SingletonTheory.OrmLite.Providers;

namespace ServiceStack.OrmLite.Tests
{
	[TestFixture]
	public class SqlProvidersTests
	{
		[Test]
		public void Shippers_UseCase()
		{
			int trainsTypeId, planesTypeId;

			using (SqlProvider db = new SqlProvider(ConfigSettings.ConnectionString, false))
			{
				db.DropAndCreate<ShipperType>();
				db.DropAndCreate<Shipper>();
			}

			using (SqlProvider db = new SqlProvider(ConfigSettings.ConnectionString, true))
			{
				// Playing with transactions
				db.Delete<ShipperType>(x => x.Name == "Trains" || x.Name == "Planes");

				trainsTypeId = (int)db.Insert(new ShipperType { Name = "Trains" });
				planesTypeId = (int)db.Insert(new ShipperType { Name = "Planes" });
			}

			using (SqlProvider db = new SqlProvider(ConfigSettings.ConnectionString, true))
			{
				db.Insert(new ShipperType { Name = "Automobiles" });
				Assert.That(db.Select<ShipperType>(), Has.Count.EqualTo(3));

				db.Rollback();
			}

			using (SqlProvider db = new SqlProvider(ConfigSettings.ConnectionString, true))
			{
				Assert.That(db.Select<ShipperType>(), Has.Count.EqualTo(2));

				// Performing standard Insert's and Selects
				db.Insert(new Shipper { CompanyName = "Trains R Us", Phone = "555-TRAINS", ShipperTypeId = trainsTypeId });
				db.Insert(new Shipper { CompanyName = "Planes R Us", Phone = "555-PLANES", ShipperTypeId = planesTypeId });
				db.Insert(new Shipper { CompanyName = "We do everything!", Phone = "555-UNICORNS", ShipperTypeId = planesTypeId });

				var trainsAreUs = db.Single<Shipper>("ShipperTypeId = {0}", trainsTypeId);
				Assert.That(trainsAreUs.CompanyName, Is.EqualTo("Trains R Us"));
				Assert.That(db.Select<Shipper>("CompanyName = {0} OR Phone = {1}", "Trains R Us", "555-UNICORNS"), Has.Count.EqualTo(2));
				Assert.That(db.Select<Shipper>("ShipperTypeId = {0}", planesTypeId), Has.Count.EqualTo(2));

				// Lets update a record
				trainsAreUs.Phone = "666-TRAINS";
				db.Update(trainsAreUs);
				Assert.That(db.GetById<Shipper>(trainsAreUs.Id).Phone, Is.EqualTo("666-TRAINS"));

				// Then make it dissappear
				db.Delete<Shipper>(trainsAreUs);

				Shipper deletedItem = db.GetById<Shipper>(trainsAreUs.Id);
				Assert.IsNull(deletedItem);

				// And bring it back again
				db.Insert(trainsAreUs);

				// Performing custom queries
				// Select only a subset from the table
				var partialColumns = db.Select<SubsetOfShipper>(typeof(Shipper), "ShipperTypeId = {0}", planesTypeId);
				Assert.That(partialColumns, Has.Count.EqualTo(2));

				// Select into another POCO class that matches sql
				var rows = db.Select<ShipperTypeCount>("SELECT ShipperTypeId, COUNT(*) AS Total FROM Shippers GROUP BY ShipperTypeId ORDER BY COUNT(*)");

				Assert.That(rows, Has.Count.EqualTo(2));
				Assert.That(rows[0].ShipperTypeId, Is.EqualTo(trainsTypeId));
				Assert.That(rows[0].Total, Is.EqualTo(1));
				Assert.That(rows[1].ShipperTypeId, Is.EqualTo(planesTypeId));
				Assert.That(rows[1].Total, Is.EqualTo(2));

				// And finally lets quickly clean up the mess we've made:
				db.DeleteAll<Shipper>();
				db.DeleteAll<ShipperType>();

				Assert.That(db.Select<Shipper>(), Has.Count.EqualTo(0));
				Assert.That(db.Select<ShipperType>(), Has.Count.EqualTo(0));
			}
		}
	}
}