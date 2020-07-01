using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using CSharp8Examples.Helper;

namespace CSharp8Examples
{
    class Program
    {
        static Country country = new Country();
        static List<Country> countryList = new List<Country>();

        static void Main(string[] args)
        {           
            country.name = "China";
            country.hemisphere = "east";
            countryList.Add(country);


            // Property pattern
            var government = GetGovernmentType();
            Console.WriteLine("Property pattern");
            Console.WriteLine(government);
            Console.ReadLine();
        }

        #region // Property pattern
        static governmentType GetGovernmentType() =>
            country switch
            {
                { name: "USA" } => governmentType.Democracy,
                { name: "China" } => governmentType.Communist,
                { name: "Venenzuela" } => governmentType.Dictatorship,
                { name: "Canada" } => governmentType.Socialist,
                _ => governmentType.Unknown
            };
        #endregion

        #region // Switch expression
        static string GetHemisphere(Nation nation) =>
            nation switch
            {
                Nation.USA => Entity.Hemisphere.West.ToString(),
                Nation.China => Entity.Hemisphere.East.ToString(),
                Nation.Germany => Entity.Hemisphere.West.ToString(),
                Nation.Mexico => Entity.Hemisphere.West.ToString(),
                _ => Entity.Hemisphere.Unknown.ToString()
            };
        #endregion

        public enum governmentType
        {
            Democracy,
            Communist,
            Socialist,
            Dictatorship,
            Unknown
        }

        public enum Nation
        {
            China,
            USA,
            Germany,
            Mexico
        }
    }

    public partial class Entity
    {
        public enum Hemisphere
        {
            East,
            West,
            Unknown
        }
    }
}
