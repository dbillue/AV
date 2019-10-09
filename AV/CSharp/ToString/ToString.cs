/***********************************
Author:			Duane Billue
Date: 			20190527
Description:	ToString override
File Name:		ToString.cs
***********************************/
using System;

namespace ToString
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("ToString Begin: {0}", System.DateTime.Now.ToString());
			
			// Assign and write current age to console.
			int age = 44;
			Console.WriteLine("My age: {0}", age.ToString());

			// Write trainee name using overridden ToString() from class Trainee().
			Trainee trainee = new Trainee
			{
				name = "Duane"
			};
			Console.WriteLine("Trainee name: {0}", trainee); 
			
			// Debug.
			Console.WriteLine("ToString End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();	
		}
	}
	
	public class Trainee
	{
		public string name = string.Empty;
		public override string ToString() => name;		// Override ToString().
	}
}