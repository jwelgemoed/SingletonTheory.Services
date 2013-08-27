using SingletonTheory.Library.Interfaces;
using System;

namespace SingletonTheory.Library.Tests.TestObjects
{
	public class TestProcessor : IProcessor
	{
		#region IProcessor Members

		public void Start()
		{
			throw new NotImplementedException();
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
