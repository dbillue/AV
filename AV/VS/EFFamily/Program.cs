using System;
using System.Collections.Generic;
using System.Linq;

namespace EFFamily
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Person> personList = null;
            IEnumerable<Family> familyList = null;

            #region // Simple select on Person table.
            using (var db = new AV_Fam_APIEntities())
            {
                personList = from p in db.Persons orderby p.LastName select p;

                foreach (var person in personList)
                {
                    Console.WriteLine(person.FirstName + " " + person.LastName);
                }
            }
            #endregion

            Console.ReadLine();
            Console.Clear();

            #region // Simple inner join Persons and Pets table
            using (var db = new AV_Fam_APIEntities())
            {
                familyList = from persons in db.Persons
                             join pets in db.Pets on persons.PersonId equals pets.PersonId
                             select new Family
                             {
                                 Name = persons.FirstName + " " + persons.LastName,
                                 PetName = pets.Name
                             };

                foreach (var person in familyList)
                {
                    Console.WriteLine(person.Name + Environment.NewLine + "  " + person.PetName);
                }
            }
            #endregion

            Console.ReadLine();
            Console.Clear();

            // Complex inner join on Persons, Pets and BirthState tables.
            using (var db = new AV_Fam_APIEntities())
            {
                familyList = from persons in db.Persons
                             join pets in db.Pets on persons.PersonId equals pets.PersonId
                             join state in db.BirthStates on persons.StateId equals state.StateId
                             orderby state.State, persons.LastName
                             select new Family
                             {
                                 Name = persons.FirstName + " " + persons.LastName,
                                 PetName = pets.Name,
                                 StateName = state.State
                             };

                foreach (var person in familyList)
                {
                    Console.WriteLine(person.Name + ":" + person.StateName + Environment.NewLine + "  " + person.PetName);
                }
            }

            Console.ReadLine();
            Console.Clear();
        }
    }

    public class Family
    {
        public string Name { get; set; }

        public string PetName { get; set; }

        public string StateName { get; set; }
    }
}
