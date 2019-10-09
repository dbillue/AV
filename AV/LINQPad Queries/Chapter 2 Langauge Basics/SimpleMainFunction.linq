<Query Kind="Program" />

/***********************************
Author:			Duane Billue
Date: 			20190523
Description:	C# 7.0 Nutshell
Chapter:		Language Basics: 2
File Name:		SimpleMainFunction.linq
***********************************/

static int Main()
{
	int success = 0;
	
	try
	{
		Console.WriteLine("yup {0}", "yup");
//		throw new Exception("error");
	} catch (Exception) {
		success = 1;
	} finally {
		//TODO: Duane Billue - Add finally wrap up code.
	}
	return success;
}