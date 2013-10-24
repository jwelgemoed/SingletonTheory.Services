using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using SingletonTheory.OrmLite.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SingletonTheory.OrmLite.Tests.Data
{
	public class ShipperContact : IChildEntity<long>
	{
		#region Fields & Properties

		[AutoIncrement]
		[Alias("Id")]
		public long Id { get; set; }

		//[References(typeof(Shipper))]
		[ForeignKey(typeof(Shipper), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
		public long ParentId { get; set; }

		[Required]
		[Index(Unique = true)]
		[StringLength(10)]
		public string PhoneNumber { get; set; }

		[Required]
		[Index(Unique = true)]
		[EmailAddress()]
		public string EmailAddress { get; set; }

		#endregion Fields & Properties

		#region IIdentifiable Members

		public void SetId(long id)
		{
			Id = id;
		}

		#endregion IIdentifiable Members
	}
}
