/***********************************
Author:			Duane Billue
Date: 			20190609
Description:	Anonymous Pipes Client
File Name:		AnonymousPipesClient.cs
***********************************/
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace AnonymousPipesClient
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				// Debug.
				Console.WriteLine("Anonymous Pipes Client Begin: {0}", System.DateTime.Now.ToString());
				
				//string cltID = args[0];
				//string svrID = args[1];
				
				// Debug.
				Console.WriteLine($"args.Length: {args.Length}");
				
				// Debug.
				if(args.Length > 0)
				{
					for(int i = 0; i < args.Length; i++)
					{
						Console.WriteLine($"args[{i}]: {args[i].ToString()}");
					}
				}
				
				// Debug.
				Console.WriteLine("Line #40");
				
				using(var clt = new AnonymousPipeServerStream(PipeDirection.In, inherit))
				using(var svr = new AnonymousPipeServerStream(PipeDirection.Out, inherit))
				{
					// Debug.
					Console.WriteLine("AnonymousPipesClient.cs :: Line #46");
					
					Console.WriteLine($"Transmission received: {clt.ReadByte()}");
					svr.WriteByte(200);
				}
			} 
			catch(Exception ex) 
			{
				Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source);
			}
			
			// Debug.
			Console.WriteLine("Anonymous Pipes Client End: {0}", System.DateTime.Now.ToString());
		}
	}
}