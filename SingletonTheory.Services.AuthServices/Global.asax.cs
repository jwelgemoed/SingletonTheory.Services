using System;

namespace SingletonTheory.Services.AuthServices
{
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Initialize your application singleton
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Application_Start(object sender, EventArgs e)
		{
			new AppHost().Init();
		}
	}
}