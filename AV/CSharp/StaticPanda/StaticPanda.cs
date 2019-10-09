/***********************************
Author:			Duane Billue
Date: 			20190525
Description:	Static versus Instance members
File Name:		StaticPanda.cs
***********************************/
using System;

namespace StaticPanda
{
	public class Test
	{
		static int success = 0;

		public static int Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Static Panda Begin: {0}", System.DateTime.Now.ToString());

			//  Create and build Panda(s).
			Panda panda1 = new Panda("Duane");
			Panda panda2 = new Panda("Allie");

			// Write out panda names. **Instance fields**
			Console.WriteLine("Panda Names: {0}, :: {1}", panda1.name, panda2.name);

			// Write out panda population. **Static field**
			Console.WriteLine("Panda population: {0}", Panda.population);

			// Debug.
			Console.WriteLine("Static Panda End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();

			// Return succes.
			return success;
		}
	}

	class Panda
	{
		public string name;					// Instance field.
		public static int population;		// Static field.

		public Panda(string n)				// CTOR.
		{
			name = n;						// Assign instance field.	
			population = population + 1;	// Assign static field and incrment by one.
		}
	}
}