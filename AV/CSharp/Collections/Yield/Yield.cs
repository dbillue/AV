/***********************************
Author:			Duane Billue
Date: 			20190602
Description:	Yield Key Word
File Name:		Yield.cs
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
			Console.WriteLine("Yield Begin: {0}", System.DateTime.Now.ToString());
			
			// Write numbers using keyword yield.
			GenCollection genCollection = new GenCollection();
			
			foreach(int i in genCollection.GetNumbers())
			{
				Console.WriteLine($"{i}");
			}

			// Debug.
			Console.WriteLine("Yield End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();	
		}
	}

	public class GenCollection
	{		
		public IEnumerable GetNumbers()
		{
			yield return 1;
			yield return 2;
			yield return 3;
		}
	}
}