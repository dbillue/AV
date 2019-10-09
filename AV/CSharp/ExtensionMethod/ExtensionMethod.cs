/***********************************
Author:			Duane Billue
Date: 			20190528
Description:	Extension Method
File Name:		ExtensionMethod.cs
***********************************/
using System;

namespace ExtensionMethod
{
	using Utils;
	
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Extension Method Begin: {0}", System.DateTime.Now.ToString());
			
			// Call extension method.
			var extMethodVal = "Georgia".IsCapitalized();
			
			Console.WriteLine("extMethodVal: {0}", extMethodVal);
			
			// Debug.
			Console.WriteLine("Extension Method End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}

namespace Utils
{
	public static class StringHelper
	{
		public static bool IsCapitalized(this string state)		// Extension method using 'this' keyword on first 
		{														// method parameter.
			if(string.IsNullOrEmpty(state)) return false;
			return char.IsUpper(state[0]);
		}
	}
}