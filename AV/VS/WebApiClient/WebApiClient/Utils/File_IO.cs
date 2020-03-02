using System;
using System.IO;

namespace WebApiClient.Utils
{
	public class File_IO
	{
		public string FileName { get; set; }

		public string FilePath { get; set; }

		public string FullFilePath { get; set; }

		public File_IO() {}

		public bool WriteFile(string fullFilePath, string data)
		{
			if(String.IsNullOrEmpty(data))
			{
				Console.WriteLine("No data to write to file.");
				return false;
			} else {
				if(!FileExists(fullFilePath))
				{
					File.Create(fullFilePath);
				}

				File.WriteAllText(fullFilePath, data);
				return true;
			}
		}

		public bool FileExists(string fullFilePath)
		{
			if(File.Exists(fullFilePath))
			{
				return true;
			} else{
				return false;
			}
		}
	}
}