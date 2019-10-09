/***********************************
Author:			Duane Billue
Date: 			20190602
Description:	LinkedList<T> Collection
File Name:		LinkedListCollection.cs
***********************************/
using System;
using System.Collections.Generic;

namespace ListCollection
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Linked List Collection Begin: {0}", System.DateTime.Now.ToString());

			// Create implicit LinkedList<T> of type string.
			var song = new LinkedList<string>();
			string lyrics = string.Empty;

			/******************************************/
			// Add first and last strings to LinkedList<T>.
			song.AddFirst("Pickin'");
			song.AddLast("Pieces");

			// Write string LinkedList<T> collection to console.
			Console.WriteLine("----========<*><*>========----");
			foreach(string lyric in song)
			{
				Console.Write(lyric + " " + Environment.NewLine);
			}
			Console.WriteLine("----========<*><*>========----");			
			/******************************************/

			/******************************************/
			// Add words after first string and next string to LinkedList<T>.
			song.AddAfter(song.First, "Up");
			song.AddAfter(song.First.Next, "Those");

			// Write string LinkedList<T> collection to console.
			foreach(string lyric in song)
			{
				Console.Write(lyric + " " + Environment.NewLine);
			}
			Console.WriteLine("----========<*><*>========----");
			/******************************************/

			/******************************************/
			// Addd strings to end of LinkedList<T>.
			song.AddLast("That Never Wait.");

			// Write string LinkedList<T> collection to console.
			foreach(string lyric in song)
			{
				Console.Write(lyric + " " + Environment.NewLine);
			}
			Console.WriteLine("----========<*><*>========----");
			/******************************************/

			/******************************************/
			// Addd strings to front of last string(s) in LinkedList<T>.
			song.AddBefore(song.Last, "...");

			// Write string LinkedList<T> collection to console.
			foreach(string lyric in song)
			{
				Console.Write(lyric + " " + Environment.NewLine);
			}
			Console.WriteLine("----========<*><*>========----");	
			/******************************************/

			// Debug.
			Console.WriteLine("Linked List Collection End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}