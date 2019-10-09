/***********************************
Author:			Duane Billue
Date: 			20190528
Description:	Anonymous Type
File Name:		AnonymousType.cs
***********************************/
using System;

namespace AnonymousType
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Anonymous Type Begin: {0}", System.DateTime.Now.ToString());
			
			// Anonymous Type (must use var keyword).
			var state = new { name = "California", hasCoast = true };
			
			// Anonymous Type Array (must use var keyword).
			var states = new[] 
			{ 
				new { name = "California", hasCoast = true },
				new { name = "Colorado", hasCoast = false }
			};
						 
			// Write out anonymous type to console.
			Console.WriteLine("State property: Name = {0}, Has Coast = {1}", state.name, state.hasCoast);
			
			// Debug.
			Console.WriteLine("Anonymous Type End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}