/***********************************
Author:			Duane Billue
Date: 			20190605
Description:	File Streams Synchronous
File Name:		FileStreamSynchronous.cs
***********************************/
using System;
using System.IO;

namespace FileStreamSynchronous
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("File Streams Synchronous Begin: {0}", System.DateTime.Now.ToString());
			
			// Create write to text file using FileStream.
			using(Stream s = new FileStream("StreamOutout.txt", FileMode.Create))
			{
				Console.WriteLine($"CanRead: {s.CanRead}");
				Console.WriteLine($"CanWrite: {s.CanWrite}");
				Console.WriteLine($"CanSeek: {s.CanSeek}");
				
				s.WriteByte(101);										// Write byte to file.
				s.WriteByte(102);										// Write byte to file.
				byte[] block = { 1, 2, 3, 4, 5 };						// Create array.
				s.Write (block, 0, block.Length);						// Write array data to file {data, position, length}.

				Console.WriteLine($"Length: {s.Length}");				// Write out stream properties.
				Console.WriteLine($"Position: {s.Position}");
				Console.WriteLine($"CanSeek: {s.CanSeek}");
				
				s.Position = 0;											// Set Position to 0.
				
				Console.WriteLine($"Byte 101: {s.ReadByte()}");			// Position is 0.
				Console.WriteLine($"Byte 102: {s.ReadByte()}");			// Position is 1.
				Console.WriteLine($"Stream Position: {s.Position}");
				
				Console.WriteLine("s.GetType: " + s.GetType());
				Console.WriteLine("s.Length.GetType: " + s.Length.GetType());
				Console.WriteLine("Block.GetType: " + block.GetType());
				
				// Read from text file and write to console.			
				Console.WriteLine(s.Read(block, 0, block.Length));		// Read data from stream {data, position, length}.
			}

			// Debug.
			Console.WriteLine("File Streams Synchronous End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}
