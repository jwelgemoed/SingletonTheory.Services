using ServiceStack.Text;
using System.IO;
using System.Text;

namespace SingletonTheory.Library.IO
{
	public static class SerializationUtilities
	{
		public static T ReadFile<T>(string fileName)
		{
			string serializedString = File.ReadAllText(fileName);

			return DeserializeFromJson<T>(serializedString);
		}

		public static T DeserializeFromJson<T>(string serializedString)
		{
			serializedString = CleanString(serializedString);
			T readObject = TypeSerializer.DeserializeFromString<T>(serializedString);

			return readObject;
		}

		public static string SerializeToJson(object objectToWrite, bool prettyPrint = true)
		{
			if (prettyPrint)
				return TypeSerializer.SerializeAndFormat(objectToWrite);

			return TypeSerializer.SerializeToString(objectToWrite);
		}

		public static void ReplaceFile(string fileName, object objectToWrite)
		{
			using (Stream stream = File.Open(fileName, FileMode.Create))
			{
				string serializedString = SerializeToJson(objectToWrite);
				byte[] serializedBytes = Encoding.ASCII.GetBytes(serializedString);
				stream.Write(serializedBytes, 0, serializedBytes.Length);
			}
		}

		public static void WriteToFile(string fileName, object objectToWrite)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			using (FileStream stream = fileInfo.OpenWrite())
			{
				string serializedString = SerializeToJson(objectToWrite);
				byte[] serializedBytes = Encoding.ASCII.GetBytes(serializedString);
				stream.Write(serializedBytes, 0, serializedBytes.Length);
			}
		}

		private static string CleanString(string stringToSerialize)
		{
			return stringToSerialize
				.Replace("\t", string.Empty)
				.Replace("\n", string.Empty)
				.Replace("\r", string.Empty)
				.Replace(": ", ":")
				.Replace(", ", ",");
		}
	}
}
