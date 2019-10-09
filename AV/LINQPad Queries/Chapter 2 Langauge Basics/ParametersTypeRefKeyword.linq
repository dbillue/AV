<Query Kind="Program" />

class Test
{
	static void Main()
	{
		string fname = "Duane";
		string lname = "Billue";
		Console.WriteLine($"Before swap: {fname}, {lname}");
		Swap(ref fname, ref lname);
		Console.WriteLine($"After swap: {fname}, {lname}");
	}
	
	static void Swap(ref string fname, ref string lname)
	{
		string swapVal = fname;
		fname = lname;
		lname = swapVal;
	}
}
