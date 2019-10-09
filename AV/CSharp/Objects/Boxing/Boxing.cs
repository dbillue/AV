/***********************************
Author:			Duane Billue
Date: 			20190527
Description:	Boxing and Unboxing
File Name:		Boxing.cs
***********************************/
using System;

namespace Boxing
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Boxing Begin: {0}", System.DateTime.Now.ToString());
			
			// Boxing of an int to type object.
			int x = 0;
			object obj = x;
			Console.WriteLine("Boxed variable from value type to reference type object: {0}", obj.ToString());
			
			// Unbox the variable.
			int y = (int)obj + 1;
			Console.WriteLine("UnBoxed variable from reference type to value type object: {0}", y.ToString());

			// Debug.
			Console.WriteLine("Boxing End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}