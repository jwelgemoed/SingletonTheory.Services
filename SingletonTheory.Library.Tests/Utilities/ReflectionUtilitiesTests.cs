using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingletonTheory.Library.Interfaces;
using SingletonTheory.Library.Tests.TestObjects;
using SingletonTheory.Library.Utilities;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Library.Tests.Utilities
{
	[TestClass]
	public class ReflectionUtilitiesTests
	{
		[TestMethod]
		public void ShouldLoadAllConcreteTypesImplementingIProcessor()
		{
			// Arrange

			// Act
			List<Type> implementers = ReflectionUtilities.GetInterfaceImplementations(this.GetType().Assembly, typeof(IProcessor));

			// Assert
			Assert.AreEqual(1, implementers.Count);
			Assert.AreEqual(typeof(TestProcessor).AssemblyQualifiedName, implementers[0].AssemblyQualifiedName);
		}
	}
}
