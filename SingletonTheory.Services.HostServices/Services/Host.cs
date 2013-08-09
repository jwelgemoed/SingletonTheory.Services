using System.ServiceProcess;

namespace SingletonTheory.Services.Host.Services
{
	public partial class Host : ServiceBase
	{
		public Host()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
		}

		protected override void OnStop()
		{
		}
	}
}
