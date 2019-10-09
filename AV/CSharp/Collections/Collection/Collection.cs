/***********************************
Author:			Duane Billue
Date: 			20190605
Description:	Collection<T>
File Name:		Collection.cs
***********************************/
using System;
using System.Collections.ObjectModel;

namespace StackCollection
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Debug.
			Console.WriteLine("Collection<T> Begin: {0}", System.DateTime.Now.ToString());

			Zoo zoo =  new Zoo();
			zoo.Animals.Add(new Animal ("Elephant", 7));
			zoo.Animals.Add(new Animal ("Lions", 4));
			zoo.Animals.Add(new Animal ("Panda", 9));
			zoo.Animals.Add(new Animal ("Seal", 5));
			
			// Write each animal to console.
			foreach(Animal animal in zoo.Animals) Console.WriteLine(animal.Name);
			
			// Debug.
			Console.WriteLine("Collection<T> End: {0}", System.DateTime.Now.ToString());

			// Await user input.
			Console.ReadLine();
		}
	}
	
	// BASE :: Animal class.
	public class Animal
	{
		public string Name = string.Empty;
		public int? Popularity = 0;
		
		// CTOR.
		public Animal(string name, int?  popularity)
		{
			Name = name;
			Popularity = popularity;
		}
	}
	
	public class AnimalCollection : Collection<Animal>
	{
	}
	
	public class Zoo
	{
		public readonly AnimalCollection Animals = new AnimalCollection();
	}
}