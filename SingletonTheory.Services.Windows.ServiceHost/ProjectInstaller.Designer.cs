namespace SingletonTheory.Services.Windows.ServiceHost
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ServiceHostProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.ServiceHostInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// ServiceHostProcessInstaller
			// 
			this.ServiceHostProcessInstaller.Password = null;
			this.ServiceHostProcessInstaller.Username = null;
			// 
			// ServiceHostInstaller
			// 
			this.ServiceHostInstaller.ServiceName = "ServiceHost";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
						this.ServiceHostProcessInstaller,
						this.ServiceHostInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller ServiceHostProcessInstaller;
		private System.ServiceProcess.ServiceInstaller ServiceHostInstaller;
	}
}