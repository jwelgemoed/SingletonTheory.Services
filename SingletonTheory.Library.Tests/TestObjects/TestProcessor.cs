using SingletonTheory.Library.Interfaces;
using System;

namespace SingletonTheory.Library.Tests.TestObjects
{
	public class TestProcessor : IProcessor
	{
		#region Fields & Properties

		public bool Started { get; set; }

		#endregion Fields & Properties

		#region IProcessor Members

		public void Start()
		{
			Started = true;
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}

		public void Process(object state)
		{
			throw new NotImplementedException();
		}

		#endregion IProcessor Members
	}
}
