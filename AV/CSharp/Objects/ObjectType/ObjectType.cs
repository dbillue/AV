/***********************************
Author:			Duane Billue
Date: 			20190527
Description:	Object Type and Stack
File Name:		ObjectType.cs
***********************************/
using System;

namespace ObjectType
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Object Type Begin: {0}", System.DateTime.Now.ToString());
			
			Stack stack = new Stack();
			stack.Push("sausage");				// Add object to array / stack.
			string s = (string)stack.Pop();		// Down casting.
			Console.WriteLine(s);
			
			// Debug.
			Console.WriteLine("Object Type End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();	
		}
	}
	
	public class Stack
	{
		int position = 0;
		object[] data = new object[10];
		public void Push(object obj)
		{
			data[position++] = obj;
		}
		public object Pop()
		{
			return data[--position];
		}
	}
}