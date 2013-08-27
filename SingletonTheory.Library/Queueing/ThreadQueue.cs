using SingletonTheory.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SingletonTheory.Library.Queuing
{
	/// <summary>
	/// A generic implementation of a queuing solution which allows for a multi-threaded queuing system.  
	/// It allows you to asign work to specialised command object in an asynchronous manner without having to handle synchronisation or threading.
	/// </summary>
	/// <typeparam name="T">The concrete implementation that should be called when new work comes in on the queue</typeparam>
	public class ThreadQueue<T> : IThreadQueue where T : ICommand, new()
	{
		#region Fields & Properties

		private volatile bool _disposed;
		private List<Thread> _threads = new List<Thread>();
		private MemoryQueue<object> _queue = new MemoryQueue<object>();
		private T _command;

		/// <summary>
		/// The number of unprocessed worker objects
		/// </summary>
		public int QueueCount
		{
			get
			{
				return _queue == null ? 0 : _queue.Count;
			}
		}

		#endregion Fields & Properties

		#region Constructors

		/// <summary>
		/// Constructs a default ThreadQueue with one thread
		/// </summary>
		public ThreadQueue() : this(new T()) { }

		/// <summary>
		/// Constructs a ThreadQueue with the specified number of threads
		/// </summary>
		/// <param name="command">The <seealso cref="ICommand"/> that should handle the work</param>
		public ThreadQueue(T command)
		{
			_command = command;

			Thread thread = new Thread(new ThreadStart(Executor));

			thread.Name = string.Format("Thread:[{0}] - {1}", typeof(T).FullName, _threads.Count);
			thread.IsBackground = true;
			thread.Start();

			_threads.Add(thread);
		}

		/// <summary>
		/// Constructs a ThreadQueue with the specified number of threads
		/// </summary>
		/// <param name="numberOfThreads">The number of threads that should be started up</param>
		public ThreadQueue(int numberOfThreads) : this(numberOfThreads, new T()) { }

		/// <summary>
		/// Constructs a ThreadQueue with the specified number of threads
		/// </summary>
		/// <param name="numberOfThreads">The number of threads that should be started up</param>
		/// <param name="command">The <seealso cref="ICommand"/> that should handle the work</param>
		public ThreadQueue(int numberOfThreads, T command)
		{
			if (numberOfThreads <= 0)
				throw new ArgumentOutOfRangeException("numberOfThreads", numberOfThreads, "The number of threads specified must be more than 0");

			_command = command;

			for (int i = 0; i < numberOfThreads; i++)
			{
				Thread thread = new Thread(new ThreadStart(Executor));

				thread.Name = string.Format("Thread:[{0}] - {1}", typeof(T).FullName, _threads.Count);
				thread.IsBackground = true;
				thread.Start();

				_threads.Add(thread);
			}
		}

		/// <summary>
		/// The destructor which will ensure that we handle our Disposal properly
		/// </summary>
		~ThreadQueue()
		{
			Dispose(_queue != null);
		}

		#endregion Constructors

		#region Public Methods

		/// <summary>
		/// Enqueues the item to be process by the Command Object
		/// </summary>
		/// <param name="item">The item that should be processed</param>
		public void Enqueue(object item)
		{
			if (_disposed)
				throw new ObjectDisposedException("ThreadQueue");

			_queue.Enqueue(item);
		}

		#endregion Public Methods

		#region Threading Methods

		/// <summary>
		/// The execution method for the Threads to live in.
		/// </summary>
		private void Executor()
		{
			while (!_disposed || (_queue != null && _queue.Count != 0))
			{
				try
				{
					object item = _queue.Dequeue(10000);
					if (item != null)
						_command.Execute(item);
				}
				catch (ThreadInterruptedException) { }
				catch (ThreadAbortException)
				{
					Thread.ResetAbort();
				}
				catch { }
			}
		}

		#endregion Threading Methods

		#region IDisposable Members

		/// <summary>
		/// Disposes the ThreadQueue after all items in the queue have been processed
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposes the ThreadQueue after all items in the queue have been processed
		/// </summary>
		/// <param name="disposeManagedResources">Indicates whether managed resources be disposed</param>
		private void Dispose(bool disposeManagedResources)
		{
			if (!_disposed)
			{
				_disposed = true;

				if (disposeManagedResources)
				{
					_queue.WaitOnEmpty();
					_queue.ReleaseWaitingThreads();
					_queue = null;

					_threads.Clear();
					_threads = null;

					// Do the cast with an "as" that way you don't have to cast twice when using the "is" keyword
					IDisposable disposable = _command as IDisposable;
					if (disposable != null)
						disposable.Dispose();
				}
			}
		}

		#endregion IDisposable Members
	}
}