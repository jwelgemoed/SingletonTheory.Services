using System.Configuration;

namespace SingletonTheory.Services.Windows.ServiceHost.Settings
{
	public static class ConfigSettings
	{
		public static bool DebugMode
		{
			get
			{
				bool debugMode = true;
				if (bool.TryParse(ConfigurationManager.AppSettings["DebugMode"], out debugMode))
					return debugMode;

				return debugMode;
			}
		}
	}
}