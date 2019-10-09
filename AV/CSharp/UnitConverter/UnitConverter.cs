/***********************************
Author:			Duane Billue
Date: 			20190525
Description:	Unit Converter
File Name:		UnitConverter.cs
***********************************/
using System;

namespace UnitConverter
{
	public class Test
	{
		public static int Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Unit Converter Begin: {0}", System.DateTime.Now.ToString());

			// Create UnitConverter class object(s).
			UnitConverter feetToInches = new UnitConverter(12);
			UnitConverter milesToFeet  = new UnitConverter(5280);

			// Write out conversions.
			Console.WriteLine("Feet To Inches: " + feetToInches.Convert(3));
			Console.WriteLine("Miles To Feet: " + milesToFeet.Convert(10));

			// For fun...tell the computer to beep ::))
			Console.WriteLine("Beep Beep!!!");
			Console.Beep();Console.Beep();

			// Debug.
			Console.WriteLine("Unit Converter End: {0}", System.DateTime.Now.ToString());

			// Await user input to  close program.
			Console.ReadLine();

			// Return success.
			return 0;
		}
	}

	class UnitConverter
	{
		int ratio;								// Field
		public UnitConverter(int unitRatio)		// Constructor
		{
			ratio = unitRatio;
		}
		public int Convert(int unit)			// Method
		{
			return unit * ratio;
		}
	}
}