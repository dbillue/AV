/***********************************
Author:			Duane Billue
Date: 			20190602
Description:	Array Searching
File Name:		ArraySearch.cs
***********************************/
using System;

namespace ArraySearch
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Array Search Begin: {0}", System.DateTime.Now.ToString());

			// Create array using regular syntax [] and then searchusing keyword Contains.
			string[] customers = {"Johnson Controls", "Howards Produce", "Widget Diget Inc." };

			/***************************/
			//  Array searching with contains keyword.
			string match = Array.Find(customers, ContainsLetter); 						// Call method 'ContainsLetter()'.
			Console.WriteLine ("Search using contains keyword: {0}", match);
			/***************************/

			/***************************/
			// Array searching with anonymous type.
			match = Array.Find(customers, delegate (string name) 
				{ return name.Contains("W"); });
			Console.WriteLine ("Search using anonymous type: {0}", match);
			/***************************/

			/***************************/
			// Array searching with lambda expression.
			match = Array.Find(customers, n => n.Contains("H"));
			Console.WriteLine ("Search lambda expression: {0}", match);
			/***************************/

			// Debug.
			Console.WriteLine("Array Search End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();	
		}

		// Method ContainsLetter() using 'Contains' keyword.
		public static bool ContainsLetter(string name) { return name.Contains ("o"); }   	// Finds first instance of name with letter 'o'.  Inline expression statement.
	}
}
