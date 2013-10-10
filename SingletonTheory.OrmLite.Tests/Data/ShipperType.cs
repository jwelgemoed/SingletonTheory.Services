using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;
using System.ComponentModel.DataAnnotations;

namespace MultiDatabaseSupport.Data
{
	[Alias("ShipperTypes")]
	public class ShipperType : IHasId<int>
	{
		[AutoIncrement]
		[Alias("ShipperTypeID")]
		public int Id { get; set; }

		[Required]
		[Index(Unique = true)]
		[StringLength(40)]
		public string Name { get; set; }
	}
}