using SingletonTheory.Library.Interfaces;

namespace SingletonTheory.Library.Tests.Commands
{
	#region Delegates

	public delegate void OnExecuteDelegate(object state);

	#endregion Delegates

	public class ThreadCommand : ICommand
	{
		#region Events

		public static event OnExecuteDelegate OnExecute;

		#endregion Events

		#region ICommand Members

		public void Execute(object state)
		{
			if (state == null)
				return;

			if (OnExecute != null)
				OnExecute(state);
		}

		#endregion ICommand Members
	}
}
