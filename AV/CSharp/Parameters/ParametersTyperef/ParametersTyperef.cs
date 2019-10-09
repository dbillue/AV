/***********************************
Author:			Duane Billue
Date: 			20190525
Description:	Parameters - ref keyword
File Name:		ParametersTyperef.cs
***********************************/
using System;

namespace ParametersTyperef
{
	public class X
	{
		public static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Parameters - ref Keyword Begin: {0}", System.DateTime.Now.ToString());

			// Assing value type int variable
			int x = 8;
			
			//Write out variable value using string Interpolation.
			Console.WriteLine($"Variable value before modifying using ref keyword: {x}");
			
			// Call method using ref keyword.
			ParamMethodRef(ref x);

			//Write out variable value using string Interpolation.
			Console.WriteLine($"Variable value after modifying using ref keyword: {x}");

			// Debug.
			Console.WriteLine("Parameters - ref Keyword End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}

		static void ParamMethodRef(ref int x)
		{
			x = x + 10;
		}
	}
}