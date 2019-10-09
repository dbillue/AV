/***********************************
Author:			Duane Billue
Date: 			20190527
Description:	Delegates
File Name:		Delegates.cs
***********************************/
using System;

namespace ToString
{
	public class Program
	{
		delegate int Transformer (int x);

		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Delegates Begin: {0}", System.DateTime.Now.ToString());

			// Delegate call #1.
			Transformer t = Square;											// Create delegate instance.
			int result = t(3);												// Invoke delegate.
			Console.WriteLine("Value of delegate: {0}", result);			// Number 9.

			// Debug.
			Console.WriteLine("Generics End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();				
		}

		static int Square (int x) => x * x;
	}
}