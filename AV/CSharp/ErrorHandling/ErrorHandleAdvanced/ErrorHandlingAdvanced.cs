/***********************************
Author:			Duane Billue
Date: 			20190528
Description:	Try Catch Throw Advanced
File Name:		ErrorHandlingAdvanced.cs
***********************************/
using System;
using System.Net;

namespace ErrorHandleAdvanced
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Try Catch Throw Advanced Begin: {0}", System.DateTime.Now.ToString());

			string URL = "http://www.ibm.com";
			string responseHTML = string.Empty;

			using(WebClient wc = new WebClient())
			{
				// Fetch IBM.com.
				try
				{
					responseHTML = wc.DownloadString(URL);
				} catch (WebException ex) {
					if(ex.Status == WebExceptionStatus.Timeout) 									// If statement to check for WebExceptionStatus error type.			
					{
						Console.WriteLine("Timeout occured.");
					} else {
						Console.WriteLine("ex.StackTrace");
					}
				} finally {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(responseHTML);
				}

				Console.WriteLine("***********************************" 
									+ Environment.NewLine 
									+ "***********************************");

				// Fetch Microsoft.com
				try
				{
					responseHTML = wc.DownloadString("http://www.microsoft.com");
				} catch (WebException ex) when (ex.Status == WebExceptionStatus.ConnectFailure) {	// when keyword to check for WebExceptionStatus error.		
					Console.WriteLine("Connection failure.");
				} finally {
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(responseHTML);
					Console.ForegroundColor = ConsoleColor.White;
				}
			}

			// Debug.
			Console.WriteLine("Try Catch Throw Advanced End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}
