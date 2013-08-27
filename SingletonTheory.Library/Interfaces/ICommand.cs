namespace SingletonTheory.Library.Interfaces
{
	public interface ICommand
	{
		/// <summary>
		/// The method that will be called to execute the state
		/// </summary>
		/// <param name="state">The state that should be processed</param>
		void Execute(object state);
	}
}