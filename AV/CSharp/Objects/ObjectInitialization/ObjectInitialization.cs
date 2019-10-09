/***********************************
Author:			Duane Billue
Date: 			20190526
Description:	Object Initialization
File Name:		ObjectInitialization.cs
***********************************/
using System;

namespace ObjectInitialization
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Object Initialization Begin: {0}", System.DateTime.Now.ToString());

			// Object instantion(s).
			Order order1 = new Order("AV00001");		// Instance 1

			Order order2 = new Order("AV00002");		// Instance 2
			order2.OrderNumber = "AV00002";
			order2.OrderDate = DateTime.Now;
			order2.OrderTotal = 137.59;
			order2.HasShip = true;

			Order order3 = order2;						// Instance 3
			order3.OrderNumber = "AV000023";

			Order order4 = new Order					// Instance 4
			{
				OrderNumber = "AV00004",
				OrderDate = DateTime.Now,
				OrderTotal = 110.00,
				HasShip = false,
			};
			
			//Write order numbers to console.
			Console.WriteLine("Order 1: {0} {1}Order 2: {2} {3}Order 3: {4} {5}Order 4: {6}",
								order1.OrderNumber, 
								Environment.NewLine,
								order2.OrderNumber, 
								Environment.NewLine,
								order3.OrderNumber, 
								Environment.NewLine,
								order4.OrderNumber);

			// Debug.
			Console.WriteLine("Object Initialization End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();	
		}
	}

	public class Order
	{
		public string OrderNumber = string.Empty;
		public DateTime OrderDate;
		public double OrderTotal = 0;
		public bool HasShip = false;

		public Order () {}													// CTOR 1

		public Order(string orderNumber) { OrderNumber = orderNumber; }		// CTOR 2

		public Order (														// CTOR 3
			string orderNumber,
			DateTime orderDate,
			double orderTotal,
			bool hasShip)
			{
				OrderNumber = orderNumber;
				OrderDate = orderDate;
				OrderTotal = orderTotal;
				HasShip = hasShip;
			}
	}
}