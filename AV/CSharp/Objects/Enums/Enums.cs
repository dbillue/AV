/***********************************
Author:			Duane Billue
Date: 			20190527
Description:	Enums
File Name:		Enums.cs
***********************************/
using System;

namespace ToString
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Enums Begin: {0}", System.DateTime.Now.ToString());

			// Obtain Top enum value and perform comparison.  Write comparison value to console.
			BorderSide1 topSide = BorderSide1.Top;
			bool isTop = (topSide == BorderSide1.Top);
			Console.WriteLine("isTop: {0}", isTop);

			// Enum Conversion.
			int i = (int)BorderSide1.Left;										// Assigns underlying numeric placement of enum value.
			BorderSide1 side = (BorderSide1)i;
			bool leftOrRight = (int)side <= 2;
			Console.WriteLine("leftOrRight: {0}", leftOrRight.ToString());		// True

			// Debug.
			Console.WriteLine("Enums End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();				
		}
	}

	// Enum 1
	public enum BorderSide1
	{
		Left, Right, Top, Bottom						// 0, 1, 2 ,3, 4 (Numeric value of placement).
	}

	// Enum 2
	public enum BorderSide2 : byte
	{
		Left, Right, Top, Bottom						// 0 = Left, 1 = Right, 2 = Top, 3 = Bottom (Numeric value of placement).
	}

	// Enum 3
	public enum BorderSide3 : byte
	{
		Left = 2, Right = 4, Top = 10, Bottom = 20		// 0 : Left = 2, 1 : Right = 4 , 2 : Top = 10, 3 : Bottom = 20 (Numeric value of placement and enum actual value.).
	}
}