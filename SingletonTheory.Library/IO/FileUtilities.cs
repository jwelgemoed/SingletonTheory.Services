using System;
using System.IO;

namespace SingletonTheory.Library.IO
{
	public static class FileUtilities
	{
		public static void AssertDirectoryExists(string filePath)
		{
			if (!Directory.Exists(filePath))
				throw new ArgumentException(string.Format("Specified file path does not exist - [{0}]", filePath));
		}

		public static void AssertFileExists(string fileName)
		{
			if (!File.Exists(fileName))
				throw new InvalidOperationException(string.Format("Given directory does not exist - [{0}]", fileName));
		}
	}
}
