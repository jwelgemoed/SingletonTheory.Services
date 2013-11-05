using ServiceStack.MiniProfiler;
using SingletonTheory.Services.AuthServices.Host;
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

		protected void Application_BeginRequest(object src, EventArgs e)
		{
			if (Request.IsLocal)
				Profiler.Start();
		}

		protected void Application_EndRequest(object src, EventArgs e)
		{
			Profiler.Stop();
		}
	}
}