using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingletonTheory.Library.Tests.TestObjects;
using System;
using System.Threading;

namespace SingletonTheory.Library.Queuing.Tests
{
	[TestClass]
	public class MemoryQueueTests
	{
		#region Fields & Properties

		private ManualResetEvent _resetEvent = new ManualResetEvent(false);

		#endregion Fields & Properties

		#region Test Methods

		[TestMethod]
		public void ShouldEnqueue()
		{
			MemoryQueue<MyTestObject> queue = new MemoryQueue<MyTestObject>();

			queue.Enqueue(new MyTestObject());

			Assert.AreEqual(1, queue.Count);
		}

		[TestMethod]
		public void ShouldDequeue()
		{
			MemoryQueue<MyTestObject> queue = new MemoryQueue<MyTestObject>();

			queue.Enqueue(new MyTestObject());

			Assert.AreEqual(1, queue.Count);

			object myObject = queue.Dequeue();

			Assert.IsInstanceOfType(myObject, typeof(MyTestObject));
		}

		[TestMethod]
		public void ShouldDequeueWithIntTimeout()
		{
			int expected = 1000;
			MemoryQueue<MyTestObject> queue = new MemoryQueue<MyTestObject>();

			DateTime startDateTime = DateTime.UtcNow;
			queue.Dequeue(expected);
			DateTime endDateTime = DateTime.UtcNow;

			TimeSpan timeSpace = endDateTime - startDateTime;
			int actual = Convert.ToInt32(timeSpace.TotalMilliseconds);

			// We divide to ensure that a millisecond or so won't make a difference to our test result.
			expected = expected / 100;
			actual = actual / 100;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ShouldDequeueWithTimeSpanTimeout()
		{
			int expected = 1000;
			MemoryQueue<MyTestObject> queue = new MemoryQueue<MyTestObject>();

			TimeSpan timespan = new TimeSpan(0, 0, 0, 0, expected);
			queue.Dequeue(expected);

			int actual = Convert.ToInt32(timespan.TotalMilliseconds);

			// We divide to ensure that a millisecond or so won't make a difference to our test result.
			expected = expected / 100;
			actual = actual / 100;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ShouldClear()
		{
			int expected = 0;
			MemoryQueue<MyTestObject> queue = new MemoryQueue<MyTestObject>();

			queue.Enqueue(new MyTestObject());

			Assert.AreEqual(1, queue.Count);

			queue.Clear();

			Assert.AreEqual(expected, queue.Count);
		}

		[TestMethod]
		public void ShouldPeek()
		{
			MemoryQueue<MyTestObject> queue = new MemoryQueue<MyTestObject>();
			MyTestObject actualObj = new MyTestObject();

			queue.Enqueue(actualObj);

			Assert.AreEqual(1, queue.Count);

			MyTestObject testObj = queue.Peek();

			Assert.AreEqual(1, queue.Count);
			Assert.AreEqual(actualObj.SomeValue, testObj.SomeValue);
		}

		[TestMethod]
		public void ShouldReleaseWaitingThreads()
		{
			Thread thread = new Thread(new ThreadStart(ThreadMethod));

			thread.IsBackground = true;
			thread.Start();

			MemoryQueue<MyTestObject> queue = new MemoryQueue<MyTestObject>();

			queue.ReleaseWaitingThreads();

			_resetEvent.WaitOne();
		}

		#endregion Test Methods

		#region Other Methods

		private void ThreadMethod()
		{
			_resetEvent.Set();
		}

		#endregion Other Methods
	}
}