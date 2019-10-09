/***********************************
Author:			Duane Billue
Date: 			20190609
Description:	Anonymous Pipes Server
File Name:		AnonymousPipesServer.cs
***********************************/
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace AnonymousPipesServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				// Debug.
				Console.WriteLine("Anonymous Pipes Server Begin: {0}", System.DateTime.Now.ToString());

				// Path to client executable.
				string clientPath = @"C:\Source\AV\CSharp\Streams\AnonymousPipes\AnonymousPipesClient.exe";

				HandleInheritability inherit = HandleInheritability.Inheritable;

				using(var svr = new AnonymousPipeServerStream(PipeDirection.Out, inherit))
				using(var clt = new AnonymousPipeServerStream(PipeDirection.In, inherit))
				{
					string svrID = svr.GetClientHandleAsString();
					string cltID = clt.GetClientHandleAsString();

					ProcessStartInfo prc = new ProcessStartInfo
					{
						FileName = clientPath,
						Arguments = svrID + " " + cltID,
						UseShellExecute = false
					};
					Process.Start(prc);
					
					// var startInfo = new ProcessStartInfo(clientPath, svrID + " " + cltID);
					// startInfo.UseShellExecute = false;
					// Process p =  Process.Start(startInfo);
					
					svr.DisposeLocalCopyOfClientHandle();
					clt.DisposeLocalCopyOfClientHandle();
					
					svr.WriteByte (101);
					Console.WriteLine($"Transmission received: {clt.ReadByte()}");
					
					//prc.WaitForExit();
				}
			}
			catch (Exception ex) 
			{
				Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source);
			}

			// Debug.
			Console.WriteLine("Anonymous Pipes Client End: {0}", System.DateTime.Now.ToString());			

			// Await user input.
			Console.ReadLine();
		}
	}
}
