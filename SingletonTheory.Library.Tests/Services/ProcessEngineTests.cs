using NUnit.Framework;
using SingletonTheory.Library.Processes;
using System.Collections.Generic;
using System.Reflection;

namespace SingletonTheory.Library.Tests
{
	[TestFixture]
	public class ServiceImplementation
	{
		[Test]
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
