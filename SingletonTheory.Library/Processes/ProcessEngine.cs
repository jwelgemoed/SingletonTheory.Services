using System;
using System.Collections.Generic;
using System.Reflection;
using SingletonTheory.Library.Interfaces;
using SingletonTheory.Library.Utilities;

namespace SingletonTheory.Library.Processes
{
	public class ProcessEngine
	{
		#region Fields & Properties

		private List<Assembly> _assembliesToLoad;

		public List<Type> Processors { get; set; }
		public List<IProcessor> ProcessorInstances { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public ProcessEngine(List<Assembly> assembliesToLoad)
		{
			_assembliesToLoad = assembliesToLoad;
			Processors = new List<Type>();
		}

		#endregion

		#region Processing Methods

		public void Start(List<string> excludeList)
		{
			for (int i = 0; i < _assembliesToLoad.Count; i++)
			{
				Processors.AddRange(ReflectionUtilities.GetInterfaceImplementations(_assembliesToLoad[i], typeof(IProcessor)));
			}

			StartProcessors(excludeList);
		}

		public void Stop()
		{
			for (int i = 0; i < ProcessorInstances.Count; i++)
			{
				ProcessorInstances[i].Stop();
			}
		}

		#endregion Processing Methods

		#region Private Methods

		private void StartProcessors(List<string> excludeList)
		{
			for (int i = 0; i < Processors.Count; i++)
			{
				if (!excludeList.Contains(Processors[i].Name))
				{
					IProcessor processorInstance = Activator.CreateInstance(Processors[i]) as IProcessor;
					processorInstance.Start();
					ProcessorInstances.Add(processorInstance);
				}
			}
		}

		#endregion Private Methods
	}
}
