/***********************************
Author:			Duane Billue
Date: 			20190529
Description:	Process Start
File Name:		StartProcess.cs
***********************************/
using System;
using System.IO;
using System.Diagnostics;

namespace StartProcess
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Processs Begin: {0}", System.DateTime.Now.ToString());
			
			// Start an app.
			Console.WriteLine("Opening win explorer application.");
			Process.Start(@"C:\windows\explorer.exe");
			
			// Await user input.
			Console.ReadLine();
			
			// Start an app by opening a file.
			Console.WriteLine("Opening console application.");
			Process.Start(@"C:\Source\AV\CSharp\ConsoleOut\out.txt");
			
			ProcessStartInfo prc = new ProcessStartInfo
			{
				FileName = "cmd.exe",
				Arguments = "/c ipconfig /all > C:\\Source\\AV\\CSharp\\StartProcess\\IpConfig.txt",
				UseShellExecute = false
			};
			Process.Start(prc);
			
			// Debug.
			Console.WriteLine("Process End: {0}", System.DateTime.Now.ToString());
			
			// Await user input.
			// Console.ReadLine();
		}
	}
}
