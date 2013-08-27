using SingletonTheory.Services.Windows.ServiceHost.Forms;
using SingletonTheory.Services.Windows.ServiceHost.Settings;
using System.ServiceProcess;
using System.Windows.Forms;

namespace SingletonTheory.Services.Windows.ServiceHost
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			if (ConfigSettings.DebugMode)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm(new ServiceHost()));
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] 
				{ 
					new ServiceHost() 
				};
				ServiceBase.Run(ServicesToRun);
			}
		}
	}
}
