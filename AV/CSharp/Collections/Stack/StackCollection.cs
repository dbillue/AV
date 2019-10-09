/***********************************
Author:			Duane Billue
Date: 			20190605
Description:	Stack<T> Collection
File Name:		StackCollection.cs
***********************************/
using System;
using System.Collections.Generic;

namespace StackCollection
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Stack<T> Collection Begin: {0}", System.DateTime.Now.ToString());
			
			var s = new Stack<int>();
			Console.WriteLine("----========<*><*>========----");
			s.Push(10);
			s.Push(20);
			s.Push(30);
			Console.WriteLine("Count: {0}", s.Count);
			Console.WriteLine("Peek: {0}", s.Peek());
			Console.WriteLine("Pop: {0}", s.Pop());
			Console.WriteLine("Pop: {0}", s.Pop());
			Console.WriteLine("Pop: {0}", s.Pop());
			try
			{
				Console.WriteLine($"Pop: {s.Pop()}");	// Throws an error, stack is empty.
			} catch (Exception ex) {
				Console.WriteLine("Stack is empty");
			}			
			Console.WriteLine("----========<*><*>========----");
			
			// Debug.
			Console.WriteLine("Stack<T> Collection End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}