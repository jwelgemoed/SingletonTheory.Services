using NUnit.Framework;
using SingletonTheory.Library.Tests.Commands;
using SingletonTheory.Library.Tests.TestObjects;
using System.Threading;

namespace SingletonTheory.Library.Queuing.Tests
{
	[TestFixture]
	public class ThreadQueueTests
	{
		#region Fields & Properties

		private ManualResetEvent _resetEvent = new ManualResetEvent(false);

		#endregion Fields & Properties

		#region Test Methods

		[Test]
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
			Assert.IsInstanceOf(typeof(TestObject), state);

			TestObject obj = state as TestObject;

			Assert.AreEqual("Hello World", obj.TestString);
			_resetEvent.Set();
		}

		#endregion Event Handlers
	}
}
