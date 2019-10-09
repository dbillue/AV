/***********************************
Author:			Duane Billue
Date: 			20190610
Description:	Buffered Streams
File Name:		BufferedStream.cs
***********************************/
using System;
using System.IO;
using System.Text;

namespace FileStreamer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Buffered Stream Begin: {0}", System.DateTime.Now.ToString());

			// Write 100k null bytes to a file.
			File.WriteAllBytes ("myFile.bin",  new byte[100_000]);

			using(FileStream fleStream = File.OpenRead("myFile.bin"))
			using(BufferedStream buffer = new BufferedStream(fleStream, 20_000))
			{
				Console.WriteLine($"buffer.Length (myFile.bin): {buffer.Length}");
			}

			Console.WriteLine("----========<*><*>========----");

			string flePath = @"C:\Source\AV\CSharp\Streams\FileStreams\mail.yahoo.com.html";
			
			using(FileStream fleStream = File.OpenRead(flePath))
			using(BufferedStream buffer = new BufferedStream(fleStream, 20_000))
			{
				Console.WriteLine($"buffer.Length (myFile.bin): {buffer.Length}");
			}

			// Debug.
			Console.WriteLine("Buffered Stream End: {0}", System.DateTime.Now.ToString());			

			// Await user input.
			Console.ReadLine();
		}
	}
}
	