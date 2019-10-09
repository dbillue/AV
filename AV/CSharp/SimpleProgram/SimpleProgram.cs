/***********************************
Author:			Duane Billue
Date: 			20190523
Description:	Simple Program
File Name:		SimpleProgram.cs
***********************************/
using System;

namespace SimpleProgram
{
	public class Program
	{
		public static int Main(string[] args)
		{
			int success = 0, x = 2, y = 5;
			
			try
			{
				for(int z = x; z <= y; z++)
				{
					Console.WriteLine("Value: {0}", z);
				}
				
				//throw new Exception("whoops");
			} catch (Exception  ex) {
				Console.WriteLine(ex.Message);
				success = 1;
			} finally {
				//TODO: Duane Billue - Add finally completion code.
			}
			
			Console.ReadLine();
			return success;
		}
	}
}