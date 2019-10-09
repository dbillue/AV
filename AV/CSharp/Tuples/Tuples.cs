/***********************************
Author:			Duane Billue
Date: 			20190528
Description:	Tuples
File Name:		Tuples.cs
***********************************/
using System;

namespace Tuples
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Tuples Begin: {0}", System.DateTime.Now.ToString());

			// Simple Tuple.
			var person = ("Duane", 44, "Married", true, true);

			// Use Item 'n' pattern.
			Console.WriteLine("Person: name = {0}, age = {1}, marital status: {2}, Has children: {3}, Loves parents: {4}",
								person.Item1, person.Item2, person.Item3, person.Item4, person.Item5);
								
			Console.WriteLine("***********************************" 
								+ Environment.NewLine 
								+ "***********************************");

			// Tuple with named vales.
			var person2 = (name:"Duane", age:44, maritalStatus:"Married", hasChildren:true, lovesParents:true);
			
			// Use named vales pattern.
			Console.WriteLine("Person: name = {0}, age = {1}, marital status: {2}, Has children: {3}, Loves parents: {4}",
								person2.name, person2.age, person2.maritalStatus, person2.hasChildren, person2.lovesParents);			

			// Debug.
			Console.WriteLine("Anonymous Type End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}
