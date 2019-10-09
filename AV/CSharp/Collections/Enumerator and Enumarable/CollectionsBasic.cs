/***********************************
Author:			Duane Billue
Date: 			20190530
Description:	IEnumerable IEnumerator
File Name:		CollectionsBasic.cs
***********************************/
using System;
using System.Collections;

namespace CollectionBasic
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Collection Basic Begin: {0}", System.DateTime.Now.ToString());

			/***************************
			// Interface IEnumerator.
			public interface IEnumerator;
			{
				bool MoveNext();
				object Current { get; }
				void Reset();
			}

			// Interface IEnumerable.
			// Returns a  provider of "IEnumerator".
			public interface IEnumerable
			{
				IEnumerator GetEnumerator;
			}
			*****************************/

			/***************************/
			//  Simple collection.
			int cnt = 1;
			string s = "Good Day Sir";

			// Use an Enumerable provided by Enumerator.
			IEnumerator rator = s.GetEnumerator();
			Console.WriteLine("Enumeration of string s:");
			while (rator.MoveNext())
			{
				char c = (char) rator.Current;
				Console.WriteLine("s[{0}]: {1}", cnt, c);
				cnt++;
			}
			/***************************/
			
			/***************************/
			// String class implements IEnumerable.
			cnt = 1;
			Console.WriteLine(Environment.NewLine);
			Console.WriteLine("Enumeration of string s:");
			foreach(char c in s)
			{
				Console.WriteLine("s[{0}]: {1}", cnt, c);
				cnt++;
			}
			/***************************/
			
			/***************************/
			// Arrays and historical Enumeration.
			Console.WriteLine(Environment.NewLine);
			int[] data = { 0, 1, 2, 3, 4, 5};
			foreach (var number in data)
			{
				Console.WriteLine($"number: {number}");
			}
			/***************************/
			
			// Debug.
			Console.WriteLine("Collection Basic End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}
