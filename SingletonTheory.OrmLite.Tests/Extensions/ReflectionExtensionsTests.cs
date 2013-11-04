using NUnit.Framework;
using SingletonTheory.OrmLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingletonTheory.OrmLite.Extensions;
using SingletonTheory.OrmLite.Tests.Data;

namespace SingletonTheory.OrmLite.Tests.Extensions
{
	[TestFixture]
	public class ReflectionExtensionsTests
	{
		[Test]
		public void ShouldGetTypesThatImplementsInterface()
		{
			// Act
			bool toBeAsserted = typeof(Shipper).HasInterfaceNonGeneric(typeof(IIdentifiable));

			// Assert
			Assert.IsTrue(toBeAsserted);
		}
	}
}
