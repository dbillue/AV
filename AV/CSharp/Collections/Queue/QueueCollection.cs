/***********************************
Author:			Duane Billue
Date: 			20190602
Description:	Queue<T> Collection
File Name:		QueueCollection.cs
***********************************/
using System;
using System.Collections.Generic;

namespace QueueCollection
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Queue<T> Collection Begin: {0}", System.DateTime.Now.ToString());

			// Create implicit Queue<T> of type int.
			var numbers = new Queue<int>();
			numbers.Enqueue(20);
			numbers.Enqueue(40);
			Console.WriteLine("----========<*><*>========----");
			int[] dataArray = numbers.ToArray();					// Export to an array.
			Console.WriteLine($"Count: {numbers.Count}");			// Number of items in queue.
			Console.WriteLine($"Peek: {numbers.Peek()}");				// Peek at first (head) item in queue.
			Console.WriteLine($"Dequeue: {numbers.Dequeue()}");		// Remove first (head) item in queue "20".
			Console.WriteLine($"Dequeue: {numbers.Dequeue()}");		// Remove first (head) item in queue "40".
			try
			{
				Console.WriteLine($"Dequeue: {numbers.Dequeue()}");	// Throws an error, queue is empty.
			} catch (Exception ex) {
				Console.WriteLine("Queue is empty");
			}
			Console.WriteLine("----========<*><*>========----");

			// Debug.
			Console.WriteLine("Queue<T> Collection End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}
