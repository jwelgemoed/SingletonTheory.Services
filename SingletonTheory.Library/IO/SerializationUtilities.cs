using ServiceStack.Text;
using System.IO;
using System.Text;

namespace SingletonTheory.Library.IO
{
	public static class SerializationUtilities
	{
		public static T ReadLocaleFromFile<T>(string fileName)
		{
			string serializedLocale = File.ReadAllText(fileName);
			serializedLocale = CleanString(serializedLocale);

			T readObject = TypeSerializer.DeserializeFromString<T>(serializedLocale);

			return readObject;
		}

		public static void WriteToFile(string fileName, object objectToWrite)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			using (FileStream stream = fileInfo.OpenWrite())
			{
				string serializedLocale = TypeSerializer.SerializeAndFormat(objectToWrite);
				byte[] serializedBytes = Encoding.ASCII.GetBytes(serializedLocale);
				stream.Write(serializedBytes, 0, serializedBytes.Length);
			}
		}

		private static string CleanString(string stringToSerialize)
		{
			return stringToSerialize.Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(": ", ":");
		}
	}
}
