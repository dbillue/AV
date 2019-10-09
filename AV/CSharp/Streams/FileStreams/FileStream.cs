/***********************************
Author:			Duane Billue
Date: 			20190610
Description:	File Streams
File Name:		FileStream.cs
***********************************/
using System;
using System.IO;

namespace FileStreamer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("FileStream Begin: {0}", System.DateTime.Now.ToString());
			
			using(FileStream fleStream = File.OpenRead("mail.yahoo.com.html"))
			using(TextReader txtReader = new StreamReader(fleStream))
			{
				Console.WriteLine(txtReader.ReadLine());
				Console.WriteLine(txtReader.ReadToEnd());
			}
			
			// Debug.
			Console.WriteLine("FileStream End: {0}", System.DateTime.Now.ToString());			

			// Await user input.
			Console.ReadLine();
		}
	}
}