using SingletonTheory.Library.Processes;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceProcess;

namespace SingletonTheory.Services.Windows.ServiceHost
{
	public partial class ServiceHost : ServiceBase
	{
		private ProcessEngine _engine;

		public ServiceHost()
		{
			InitializeComponent();
		}

		public void Start()
		{
			OnStart(null);
		}

		new public void Stop()
		{
			OnStop();
			base.Stop();
		}

		protected override void OnStart(string[] args)
		{
			if (_engine == null)
				_engine = new ProcessEngine(new List<Assembly> { this.GetType().Assembly });

			_engine.Start(new List<string>());
		}

		protected override void OnStop()
		{
			if (_engine != null)
				_engine.Stop();
		}
	}
}
