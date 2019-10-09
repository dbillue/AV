/***********************************
Author:			Duane Billue
Date: 			20190525
Description:	Deconstructor Pattern
File Name:		DeconstructPattern.cs
***********************************/
using System;

namespace DeconstructPattern
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Deconstruct Begin: {0}", System.DateTime.Now.ToString());

			var ticket = new Ticket(25.50, "10R");			// Constructor.
			(double price, string seat) = ticket;			// Deconstructor.

			// Write ticket properties to console.
			Console.WriteLine($"Ticket properties: {price.ToString()}, {seat}");

			// Debug.
			Console.WriteLine("Deconstruct End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();		
		}
	}

	class Ticket
	{
		// Member fields.
		public readonly double Price;
		public readonly string Seat;

		// CTOR.
		public Ticket(double price, string seat)
		{
			Price = price;
			Seat = seat;
		}

		// DECTOR.
		public void Deconstruct(out double price, out string seat)
		{
			price = Price;
			seat = Seat;
		}
	}
}