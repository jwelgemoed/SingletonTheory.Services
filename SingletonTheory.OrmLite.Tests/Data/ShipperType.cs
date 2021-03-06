﻿using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SingletonTheory.OrmLite.Tests.Data
{
	[Alias("ShipperType")]
	public class ShipperType : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		[Alias("Id")]
		public long Id { get; set; }

		[Required]
		[Index(Unique = true)]
		[StringLength(40)]
		public string Name { get; set; }

		#endregion Fields & Properties

		#region IIdentifiable Members

		public void SetId(long id)
		{
			Id = id;
		}

		#endregion IIdentifiable Members
	}
}