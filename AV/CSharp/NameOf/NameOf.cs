/***********************************
Author:			Duane Billue
Date: 			20190526
Description:	NameOf 
File Name:		NameOf.cs
***********************************/
using System;
using System.IO;

namespace NameOf
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("NameOf Begin: {0}", System.DateTime.Now.ToString());
			
			// Instantiate DirectoryInfo object.
			DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Source");
			// Write name of object initialized to DirectoryInfo.
			Console.WriteLine("Name of DirectoryInfo object: {0}", nameof(dirInfo));
			// Write name of object and property of DirectoryInfo object.
			Console.WriteLine("Name: {0}, Property: {1}", nameof(dirInfo), nameof(dirInfo.Name));
			
			// Debug.
			Console.WriteLine("NameOf End: {0}", System.DateTime.Now.ToString());
			
			// Await user input.
			Console.ReadLine();				
		}
	}
}