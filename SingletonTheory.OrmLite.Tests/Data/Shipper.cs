using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Annotations;
using SingletonTheory.OrmLite.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SingletonTheory.OrmLite.Tests.Data
{
	[Alias("Shipper")]
	public class Shipper : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		[Alias("Id")]
		public long Id { get; set; }

		[Required]
		[Index(Unique = true)]
		[StringLength(40)]
		public string CompanyName { get; set; }

		[StringLength(24)]
		public string Phone { get; set; }

		[References(typeof(ShipperType))]
		public long ShipperTypeId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(ShipperType), "ShipperTypeId")]
		public ShipperType ShipperType { get; set; }

		[Ignore()]
		[AssociatedEntityAttribute(typeof(ShipperContact), IsList = true)]
		public List<ShipperContact> ShipperContacts { get; set; }

		[Ignore()]
		public string SomeDerivedProperty { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public Shipper()
		{
			ShipperContacts = new List<ShipperContact>();
		}

		#endregion Constructors

		#region IIdentifiable Members

		public void SetId(long id)
		{
			Id = id;
		}

		#endregion IIdentifiable Members
	}
}