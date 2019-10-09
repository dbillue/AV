/***********************************
Author:			Duane Billue
Date: 			20190526
Description:	Indexers
File Name:		Indexer.cs
***********************************/
using System;

namespace Indexer
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Indexer Begin: {0}", System.DateTime.Now.ToString());

			// The this keyword allows access to the property of the array.
			Lyric lyric = new Lyric();
			var word = lyric[5];
			Console.WriteLine("Word from at position 6: {0}", word);

			// Change the word using property accessor set.
			lyric[5] = "swimmin'";
			Console.WriteLine("Word from at position 6: {0}", lyric[5]);

			// Debug.
			Console.WriteLine("Indexer End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();			
		}
	}
	
	public class Lyric
	{
		string[] words1 = "See the man coming across the field, kicking up his heals like an automobile".Split();
		string[] words2 = "You get a line, i'll get a pole".Split();
		string[] words3 = "We'll go down to the fishin' hole".Split();
		string[] words4 = "Yeah honey, baby mine".Split();
		
		public string this [int wordNum]		// Indexer w/ 'this' keyword.
		{
			get {return words3 [wordNum]; }
			set { words3 [wordNum] = value; }
		}
		
		public Lyric () {}
	}
}