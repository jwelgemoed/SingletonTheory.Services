using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;
using System.ComponentModel.DataAnnotations;

namespace MultiDatabaseSupport.Data
{
	[Alias("Shippers")]
	public class Shipper : IHasId<int>
	{
		[AutoIncrement]
		[Alias("ShipperID")]
		public int Id { get; set; }

		[Required]
		[Index(Unique = true)]
		[StringLength(40)]
		public string CompanyName { get; set; }

		[StringLength(24)]
		public string Phone { get; set; }

		[References(typeof(ShipperType))]
		public int ShipperTypeId { get; set; }
	}
}