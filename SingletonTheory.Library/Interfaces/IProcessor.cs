namespace SingletonTheory.Library.Interfaces
{
	public interface IProcessor
	{
		void Start();
		void Stop();
		void Process(object state);
	}
}
