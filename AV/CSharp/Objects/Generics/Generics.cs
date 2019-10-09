/***********************************
Author:			Duane Billue
Date: 			20190527
Description:	Generics
File Name:		Generics.cs
***********************************/
using System;
using System.Collections;

namespace ToString
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Generics Begin: {0}", System.DateTime.Now.ToString());
			
			// Use the Generic class to stack integers.
			var stack = new Stack<int>();
			stack.Push(5);						// Add object to array / stack.
			stack.Push(10);						// ...
			int x = stack.Pop();
			int y = stack.Pop();
			Console.WriteLine("int Generic: " + x.ToString() + ", " + y.ToString());
			
			// Use the Generic class to stack integers.
			var stackString = new Stack<string>();
			stackString.Push("Duane");						// Add object to array / stack.
			stackString.Push("Allie");						// ...
			string daughter = stackString.Pop();
			string dad = stackString.Pop();
			Console.WriteLine("string Generic: " + dad + ", " + daughter);		
			
			// Debug.
			Console.WriteLine("Generics End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();				
		}
	}
	
	// Generic class.
	public class Stack<T>
	{
		int position = 0;
		T[] data = new T[100];
		public void Push (T obj) 	=> data[position++] = obj;
		public T Pop()				=> data[--position];
	}
	
	// Explicitly typed class.
	public class Stack
	{
		int position = 0;
		int[] data = new int[100];									// Replace T with int.
		public void Push (int obj)	=> data[position++] = obj;		// Replace T with int.
		public int Pop()			=> data[--position];			// Replace T with int.
	}
}