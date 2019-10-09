/***********************************
Author:			Duane Billue
Date: 			20190605
Description:	HashSet<T> Collection
File Name:		HashSetCollection.cs
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
			Console.WriteLine("HashSet<T> Collection Begin: {0}", System.DateTime.Now.ToString());
			
			var signedStatement = new HashSet<char>("Document Signed on Wednesday, June 6th 2019.");
			Console.WriteLine("----========<*><*>========----");
			Console.WriteLine(signedStatement);
			Console.WriteLine($"Has letter s: {signedStatement.Contains('s')}");
			
			Console.WriteLine("----========<*><*>========----");
			foreach(char letter in signedStatement) { Console.Write(letter + Environment.NewLine); }
			
			Console.WriteLine("----========<*><*>========----");
			signedStatement.ExceptWith("aeiou");
			string letter2 = string.Empty;
			foreach(char let1 in signedStatement) { letter2 = letter2.ToString() + let1.ToString(); }
			Console.Write(letter2 + Environment.NewLine);
			
			// Debug.
			Console.WriteLine("HashSet<T> Collection End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
}