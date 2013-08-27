using System;
using System.Collections.Generic;
using System.Reflection;

namespace SingletonTheory.Library.Utilities
{
	public static class ReflectionUtilities
	{
		public static List<Type> GetInterfaceImplementations(Assembly assembly, Type interfaceType)
		{
			List<Type> processors = new List<Type>();

			Type[] types = assembly.GetTypes();
			for (int y = 0; y < types.Length; y++)
			{
				if (interfaceType.IsAssignableFrom(types[y]) && types[y].Name != interfaceType.Name)
					processors.Add(types[y]);
			}

			return processors;
		}
	}
}
