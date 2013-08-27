using System;

namespace SingletonTheory.Library.Interfaces
{
	public interface IThreadQueue : IDisposable
	{
		void Enqueue(object item);
		int QueueCount { get; }
	}
}
