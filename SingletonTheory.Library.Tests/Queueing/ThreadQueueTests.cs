using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingletonTheory.Library.Tests.Commands;
using SingletonTheory.Library.Tests.TestObjects;

namespace SingletonTheory.Library.Queuing.Tests
{
	[TestClass]
	public class ThreadQueueTests
	{
		#region Fields & Properties

		private ManualResetEvent _resetEvent = new ManualResetEvent(false);

		#endregion Fields & Properties

		#region Test Methods

		[TestMethod]
		public void ShouldCallbackOnCommand()
		{
			ThreadCommand.OnExecute += new OnExecuteDelegate(ThreadCommand_OnExecute);
			ThreadQueue<ThreadCommand> threadQueue = new ThreadQueue<ThreadCommand>();

			TestObject test = new TestObject();

			test.TestString = "Hello World";
			threadQueue.Enqueue(test);
			_resetEvent.WaitOne();
			threadQueue.Dispose();
		}

		#endregion Test Methods

		#region Event Handlers

		private void ThreadCommand_OnExecute(object state)
		{
			Assert.IsInstanceOfType(state, typeof(TestObject));

			TestObject obj = state as TestObject;

			Assert.AreEqual("Hello World", obj.TestString);
			_resetEvent.Set();
		}

		#endregion Event Handlers
	}
}
