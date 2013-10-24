namespace SingletonTheory.OrmLite.Tests.Data
{
	public static class DataProvider
	{
		public static Shipper GetShipperForInsert(bool complexScenario = false)
		{
			Shipper shipper = new Shipper();

			shipper.Id = 0;
			shipper.CompanyName = "Trains R Us";
			shipper.Phone = "555-TRAINS";

			if (complexScenario)
			{
				ShipperContact contact = new ShipperContact();
				contact.EmailAddress = "some@thing.com";
				contact.PhoneNumber = "0123456789";

				shipper.ShipperContacts.Add(contact);
			}

			return shipper;
		}

		public static ShipperType GetShipperTypesForInsert()
		{
			ShipperType shipperType = new ShipperType();

			shipperType.Name = "Trains";

			return shipperType;
		}
	}
}
