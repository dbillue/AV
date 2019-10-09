/***********************************
Author:			Duane Billue
Date: 			20190528
Description:	Nullable Types and Conversion
File Name:		NullableTypesConversion.cs
***********************************/
using System;

namespace NullableTypesConversion
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Nullable Types and Conversion Begin: {0}", System.DateTime.Now.ToString());
			
			// Example 1.
			int? x = 5;										// Nullable int. Implicit assignment/conversion.
			int y = (int)x;									// Explicit conversion.
			Console.WriteLine("x :{0}, y: {1}", x, y);
			
			// Example 2.
			object o = "string";
			int? x2 = o as int?;
			Console.WriteLine("x2.HasValue: {0}", x2.HasValue);

			// Debug.
			Console.WriteLine("Nullable Types and Conversion End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}