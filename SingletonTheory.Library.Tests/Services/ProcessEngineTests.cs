using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingletonTheory.Library.Processes;
using System.Collections.Generic;
using System.Reflection;

namespace SingletonTheory.Library.Tests
{
	[TestClass]
	public class ServiceImplementation
	{
		[TestMethod]
		public void ShouldStartProcessEngine()
		{
			// Arrange
			ProcessEngine processEngine = new ProcessEngine(new List<Assembly> { this.GetType().Assembly });

			// Act
			processEngine.Start(new List<string>());

			// Arrange
			Assert.AreEqual(1, processEngine.Processors.Count);
		}
	}
}
