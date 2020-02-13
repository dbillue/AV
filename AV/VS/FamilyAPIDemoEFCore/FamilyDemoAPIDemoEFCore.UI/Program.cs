using System;
using System.Collections.Generic;
using System.Linq;
using FamilyAPIDemoEFCore.Data;
using FamilyAPIDemoEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace FamilyAPIDemoEFCore.UI
{
    class Program
    {
        private static EFCoreStandardContext _context = new EFCoreStandardContext();

        static void Main(string[] args)
        {
            //InsertFamily();
            //InsertMulitpleFamilyMembers();
            //FamilyQuery();
            //QueryByName("Harry");
            //DeleteById(9);
            //AddFamilyQuote();
            //AddAdditionalQuote();
            EagerLoadFamliyWithQuotes();
        }

        private static void EagerLoadFamliyWithQuotes()
        {
            var familyWithQuotes = _context.Families.Where(s => s.Name.Contains("Jason"))
                                    .Include(s => s.Quotes).ToList();
        }

        private static void AddAdditionalQuote()
        {
            var family = _context.Families.First();
            family.Quotes.Add(new Quotes
            {
                Text = "God Bless America"
            });
            _context.SaveChanges();
        }

        private static void AddFamilyQuote()
        {
            var family = new Families
            {
                Name = "Jason Aldridge",
                Quotes = new List<Quotes>
                {
                    new Quotes {Text = "The power comes from within when sea shells dwell on the sea floor." }
                }
            };
            _context.Families.Add(family);
            _context.SaveChanges();
        }

        private static void DeleteById(int id)
        {
            var famMem = new Families()
            {
                Id = id
            };

            using (var context = new EFCoreStandardContext())
            {
                context.Families.Remove(famMem);
                context.SaveChanges();
            }
        }

        private static void QueryByName(string name)
        {
            var familyMember = _context.Families.Where(a => a.Name == name).FirstOrDefault();
        }

        private static void FamilyQuery()
        {
            using(var context = new EFCoreStandardContext())
            {
                var familyMembers = context.Families.ToList();
                var query = context.Families;
                foreach(var member in query)
                {
                    Console.WriteLine("Family member: {0}", member.Name);
                }
                Console.ReadLine();
            }
        }

        private static void InsertMulitpleFamilyMembers()
        {
            var family = new Families { Name = "Duane" };
            var familyFather = new Families { Name = "Charles" };
            var familyGrandFather = new Families { Name = "Harry" };
            using (var context = new EFCoreStandardContext())
            {
                context.Families.AddRange(family, familyFather, familyGrandFather);
                context.SaveChanges();
                Console.ReadLine();
            }
        }

        private static void InsertFamily()
        {
            var family = new Families { Name = "Duane" };
            using (var context = new EFCoreStandardContext())
            {
                context.Families.Add(family);
                context.SaveChanges();
                Console.ReadLine();
            }
        }
    }
}
