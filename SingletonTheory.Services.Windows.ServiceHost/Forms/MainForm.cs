using System;
using System.Windows.Forms;

namespace SingletonTheory.Services.Windows.ServiceHost.Forms
{
	public partial class MainForm : Form
	{
		private ServiceHost _service;
		private bool _started;

		public MainForm(ServiceHost serviceHost)
		{
			InitializeComponent();

			_service = serviceHost;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (!_started)
			{
				_service.Start();
				btnStart.Enabled = false;
				btnStop.Enabled = true;
			}
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			if (_started)
			{
				_service.Stop();
				btnStart.Enabled = true;
				btnStop.Enabled = false;
			}
		}
	}
}
