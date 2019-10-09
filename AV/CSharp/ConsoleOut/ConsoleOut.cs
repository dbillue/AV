/***********************************
Author:			Duane Billue
Date: 			20190529
Description:	Console Out
File Name:		ConsoleOut.cs
***********************************/
using System;
using System.IO;
using System.Diagnostics;

namespace ConsoleOut
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Console Out Begin: {0}", System.DateTime.Now.ToString());

			// Save output of console.
			TextWriter oldOut = Console.Out;
			
            using (TextWriter sw = File.CreateText(@"c:\source\av\csharp\consoleout\out.txt")) 
            {
				// Capture output.
				Console.SetOut(sw);
				Console.WriteLine("console ins and outs");
            }
			
			// Reset console output.
			Console.SetOut(oldOut);

			Console.WriteLine("Finished writing console data txt file.");
			Console.WriteLine("Press any key to view text...");
			
			// Await user input.
			Console.ReadLine();

			// Spawn notepad.
			Process.Start(@"C:\Source\AV\CSharp\ConsoleOut\out.txt");

			// Debug.
			Console.WriteLine("Console Out End: {0}", System.DateTime.Now.ToString());
		}
	}
}