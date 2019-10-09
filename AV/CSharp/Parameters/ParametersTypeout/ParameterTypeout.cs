/***********************************
Author:			Duane Billue
Date: 			20190525
Description:	Parameters - out keyword
File Name:		ParameterTypeout.cs
***********************************/
using System;

namespace ParametersTypeout
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Parameters - out Keyword Begin: {0}", System.DateTime.Now.ToString());

			// Assign reference type string variables.
			string fname = string.Empty, lname = string.Empty;
			
			// Call method using parameters with out keyword.
			SplitName("Duane Allman Billue", out fname, out lname);
			
			// Write out values.
			Console.WriteLine("fname: {0}, lname: {1}", fname, lname);

			// Debug.
			Console.WriteLine("Parameters - out Keyword End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();			
		}

		static void SplitName(string name, out string fnames, out string lname)
		{
			int i = name.LastIndexOf(' ');
			fnames = name.Substring(0, i);
			lname = name.Substring(i + 1);
		}
	}
}