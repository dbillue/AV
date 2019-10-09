/***********************************
Author:			Duane Billue
Date: 			20190526
Description:	Properties
File Name:		Properties.cs
***********************************/
using System;

namespace Properties
{
	public class Prgram
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Properties Begin: {0}", System.DateTime.Now.ToString());

			Stock av = new Stock();
			av.CurrentPrice = 35.00m;
			var stockPrice = av.CurrentPrice;
			Console.WriteLine($"AV stock price: {stockPrice}");

			// Debug.
			Console.WriteLine("Properties End: {0}", System.DateTime.Now.ToString());
			
			// Await user input.
			Console.ReadLine();
		}
	}

	public class Stock
	{
		decimal currentPrice;				// Private backing field.

		public  decimal CurrentPrice		// Public property.
		{
			get { return currentPrice; }
			set { currentPrice = value; }
		}
	}
}
