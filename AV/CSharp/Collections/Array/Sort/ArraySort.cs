/***********************************
Author:			Duane Billue
Date: 			20190602
Description:	Array Sorting
File Name:		ArraySort.cs
***********************************/
using System;

namespace ArraySort
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Array Sort Begin: {0}", System.DateTime.Now.ToString());
			
			// Simple sort.
			int[] grades = {80, 86, 92, 74};
			Array.Sort(grades);
			foreach(int grade in grades)
			{
				Console.Write($"{grade} " + Environment.NewLine);
			}
			
			// Debug.
			Console.WriteLine("Array Sort End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();	
		}			
	}
}
