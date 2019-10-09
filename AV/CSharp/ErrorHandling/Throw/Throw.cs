/***********************************
Author:			Duane Billue
Date: 			20190527
Description:	Throw
File Name:		Throw.cs
***********************************/
using System;

namespace Throw
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Throw Begin: {0}", System.DateTime.Now.ToString());

			try
			{
				Lawyer lawyer = new Lawyer();
				lawyer.Display(null);
			} catch (ArgumentNullException ex) {
				Console.WriteLine("Caught the null exception using the throw statement.");
			}

			// Debug.
			Console.WriteLine("Throw End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}

	public class Lawyer
	{
		public void Display(string name)
		{
			if(name == null)
			{
				throw new ArgumentNullException(nameof(name));
			} else {
				Console.WriteLine("Name: {0}", name);
			}
		}
	}
}