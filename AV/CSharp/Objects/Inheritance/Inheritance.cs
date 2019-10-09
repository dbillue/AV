/***********************************
Author:			Duane Billue
Date: 			20190526
Description:	Inheritance
File Name:		Inheritance.cs
***********************************/
using System;

namespace Inheritance
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Inheritance Begin: {0}", System.DateTime.Now.ToString());
			
			// Stock class.
			Stock stock = new Stock
			{
				Name = "AV",			// Inherited from Asset class
				SharesOwned = 23		// Member of Stock class
			};
			
			// House class.
			House house = new House
			{
				Name = "Cradle",		// Inherited from Asset class
				Mortgage = 239_000m		// Member of House class
			};
			
			//  Write out Stock values.
			Console.WriteLine("Stock name and shares: {0}, {1}", stock.Name, stock.SharesOwned);
			
			//  Write out House values.
			Console.WriteLine("House name and mortgage: {0}, {1}", house.Name, house.Mortgage);
			
			// Debug.
			Console.WriteLine("Inheritance End: {0}", System.DateTime.Now.ToString());
			
			// Await user input.
			Console.ReadLine();
		}
	}

	// Base Class: Asset
	public class Asset
	{
		public string Name;
	}

	// Sub-Class: House
	public class Stock : Asset
	{
		public long SharesOwned;
	}
	
	// Sub-Class: Stock
	public class House : Asset
	{
		public decimal Mortgage;
	}
}