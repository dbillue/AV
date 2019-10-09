/***********************************
Author:			Duane Billue
Date: 			20190526
Description:	Virtual keyword
File Name:		Virtual.cs
***********************************/
using System;

namespace Virtual
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Virtual Begin: {0}", System.DateTime.Now.ToString());

			// Stock class.
			Stock stock = new Stock
			{
				Name = "AV",										// Inherited from Asset class
				SharesOwned = 23									// Member of Stock class
			};

			// House class.
			House house = new House(239_000m, 51_000m);
			house.Name = "Cradle";									// Inherited from Asset class

			//  Write out Stock values.
			Console.WriteLine("Stock name and shares: {0}, {1}", stock.Name, stock.SharesOwned);

			//  Write out House values.
			Console.WriteLine("House name, mortgage, liability: {0}, {1}, {2}", house.Name, house.Mortgage, house.Liability);

			// Debug.
			Console.WriteLine("Virtual End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}

	// Base Class: Asset
	public class Asset
	{
		public string Name = string.Empty;
		public virtual decimal Liability => 0;						// Virtual member and expression bodied property.
	}

	// Sub-Class: Stock
	public class Stock : Asset
	{
		public long SharesOwned = 0;
	}

	// Sub-Class: House
	public class House : Asset
	{
		public decimal Mortgage = 0m;
		public decimal PaidOff = 0m;
		public override decimal Liability => Mortgage - PaidOff;	// Override the virtual property from base class Asset.
		
		//CTOR
		public House(decimal mortgage, decimal paidOff)
		{
			Mortgage = mortgage;
			PaidOff = paidOff;
		}
	}
}