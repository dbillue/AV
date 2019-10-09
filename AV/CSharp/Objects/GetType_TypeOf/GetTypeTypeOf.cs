/***********************************
Author:			Duane Billue
Date: 			20190527
Description:	GetType and TypeOf
File Name:		GetTypeTypeOf.cs
***********************************/
using System;
using System.Drawing;

namespace GetTypeTypeOf
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("GetType and TypeOf Begin: {0}", System.DateTime.Now.ToString());
			
			Point p = new Point();
			Console.WriteLine("p.GetType(): {0}", p.GetType());											// Obtain type.
			Console.WriteLine("typeof(Point).name: {0}", typeof(Point).Name);							// Obtain name.
			Console.WriteLine("p.GetType() == typeof(Point): {0}", p.GetType() == typeof(Point));		// Determine if types are of same value.
			Console.WriteLine("p.X.GetType().Name: {0}", p.X.GetType().Name);							// Obtain name.
			Console.WriteLine("p.Y.GetType().FullName: {0}", p.Y.GetType().FullName);					// Obtain fully qualified name.
			
			// Debug.
			Console.WriteLine("GetType and TypeOf End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();	
		}
	}
	
	public class Point
	{
		public int X = 0, Y = 0;
	}
}	