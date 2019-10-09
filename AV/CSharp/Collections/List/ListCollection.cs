/***********************************
Author:			Duane Billue
Date: 			20190602
Description:	List<T> Collection
File Name:		ListCollection.cs
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
			Console.WriteLine("List Collection Begin: {0}", System.DateTime.Now.ToString());

			// Create list of type string.
			List<string> words = new List<string>();
			string sentance = string.Empty;

			/*************************************************************************/
			// Add strings to list.
			words.Add("Excuse");
			words.Add("Me");
			words.Add("While");
			words.Add("I");
			words.Add("Work");

			// Add string range to list.
			words.AddRange(new[] { "Stop", "Talking", "You're", "Interrupting", "My", "Work." });

			// Write string List collection to console.
			foreach(string word in words)
			{
				Console.Write(word + " " + Environment.NewLine);
			}
			Console.WriteLine("----========<*><*>========----");
			/*************************************************************************/

			/*************************************************************************/
			// Insert string to list.
			words.Insert(0, "...");
			words.InsertRange(1, new[] { "Ok, Please" });

			// Write string List collection to console.
			foreach(string word in words)
			{
				sentance += word + " "; 
			}
			Console.Write(sentance + " " + Environment.NewLine);
			Console.WriteLine("----========<*><*>========----");
			/*************************************************************************/

			/*************************************************************************/
			// Remove string from list.
			words.Remove("You're");
			words.RemoveAt(0);
			words.RemoveRange(0, 1);

			// Write string List collection to console.
			sentance = string.Empty;
			foreach(string word in words)
			{
				sentance += word + " "; 
			}
			Console.Write(sentance + " " + Environment.NewLine);
			Console.WriteLine("----========<*><*>========----");
			/*************************************************************************/

			/*************************************************************************/
			// Write word count.
			Console.Write($"Word count: {words.Count} " + Environment.NewLine);
			Console.WriteLine("----========<*><*>========----");
			/*************************************************************************/
			
			/*************************************************************************/
			// Inline expression / statement.
			foreach(string word in words) Console.Write(word + " " + Environment.NewLine);
			Console.WriteLine("----========<*><*>========----");
			/*************************************************************************/
			
			/*************************************************************************/
			// New List<string> object derived from 'GetRange' keyword.
			List<string> subset = words.GetRange(0, 3);
			sentance = string.Empty;
			foreach(string word in subset)
			{
				sentance += word + " "; 
			}
			Console.Write(sentance + " " + Environment.NewLine);
			Console.WriteLine("----========<*><*>========----");			
			/*************************************************************************/

			// Debug.
			Console.WriteLine("List Collection End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}			
	}
}
