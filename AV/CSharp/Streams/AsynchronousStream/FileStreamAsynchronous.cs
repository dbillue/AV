/***********************************
Author:			Duane Billue
Date: 			20190609
Description:	File Streams Asynchronous
File Name:		FileStreamAsynchronous.cs
***********************************/
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileStreamSynchronous
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("File Streams Asynchronous Begin: {0}", System.DateTime.Now.ToString());
			
			// Call methhod member.
			AsyncStream();

			// Await user input.
			Console.ReadLine();
		}
	
		public async static void AsyncStream()
		{
			// Create write to text file using FileStream.
			using(Stream s = new FileStream("AsyncStreamOutout.txt", FileMode.Create))
			{
				byte[] block = { 1, 2, 3, 4, 5 };
				await s.WriteAsync(block, 0, block.Length);
				s.Position = 0;
				Console.WriteLine(await s.ReadAsync(block, 0, block.Length));
				
				// Debug.
				Console.WriteLine("File Streams Asynchronous End: {0}", System.DateTime.Now.ToString());
			}
		}
	}
}