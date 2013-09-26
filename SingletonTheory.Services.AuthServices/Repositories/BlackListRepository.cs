using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	// TODO:  Work out a better way of blacklisting.
	public class BlackListRepository
	{
		#region Fields & Properties

		private static List<string> _blacklist = new List<string>();

		public static List<string> Blacklist
		{
			get { return _blacklist; }
			set { _blacklist = value; }
		}

		#endregion Fields & Properties
	}
}