/***********************************
Author:			Duane Billue
Date: 			20190525
Description:	Reference Types
File Name:		ReferenceTypes.cs
***********************************/
using System;
using System.Drawing;

namespace ReferenceTypes
{
	public class RefTypes
	{
		public static void Main(string[] args)
		{
			Point p1 = new Point(7, 5);

			// Debug.
			Console.WriteLine("Reference Type Begin: {0}", System.DateTime.Now.ToString());

			// Assign p1 to p2.
			Point p2 = p1;

			// Write out reference values.
			Console.WriteLine("p1: {0} p2: {1} ", 
								p1,
								p2);	// Point 2 has same value as Point 1.
			
			// Update point 1 X value.
			p1.X = 42;
			
			// Write out reference values.
			Console.WriteLine("p1: {0} p2: {1} ", 
								p1,		// Point 1 has a new value.
								p2);	// Point 2 retain original value.			

			// Debug.
			Console.WriteLine("Reference Type End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}