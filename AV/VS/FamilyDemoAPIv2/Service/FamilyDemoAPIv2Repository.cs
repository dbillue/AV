using FamilyDemoAPIv2.DBContext;
using FamilyDemoAPIv2.Entities;
using FamilyDemoAPIv2.Helpers;
using FamilyDemoAPIv2.ResourceParameters;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FamilyDemoAPIv2.Service
{
    public class FamilyDemoAPIv2Repository : IFamilyDemoAPIv2Repository, IDisposable
    {
        private readonly FamilyDemoAPIv2Context _context;

        public FamilyDemoAPIv2Repository(FamilyDemoAPIv2Context familyDemoAPIv2Context)
        {
            _context = familyDemoAPIv2Context;
        }

        public string Response()
        {
            return "Response from service repository class FamilyDemoAPIv2Repository()";
        }

        public bool PersonExists(Guid personId)
        {
            if (personId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(personId));
            }

            return _context.Persons.Any(a => a.PersonId == personId);
        }

        public async Task AddPerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            person.PersonId = Guid.NewGuid();
            person.CreateDate = DateTime.Parse(DateTime.Now.ToString());
            await _context.Persons.AddAsync(person);
        }

        public Person GetPerson(Guid personId)
        {
            if (personId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(personId));
            }

            return _context.Persons
              .Where(c => c.PersonId == personId).FirstOrDefault();
        }

        public PagedList<Person> GetPersons(PersonResourceParameters authorsResourceParameters)
        {
            if (authorsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(authorsResourceParameters));
            }

            var collection = _context.Persons as IQueryable<Person>;

            // return _context.Persons.ToList();

            // Paging.
            return PagedList<Person>.Create(collection,
                authorsResourceParameters.PageNumber,
                authorsResourceParameters.PageSize);
        }

        public Person UpdatePerson(Person person)
        {
            return _context.Persons
              .Where(a => a.PersonId == person.PersonId).FirstOrDefault();
        }

        public void DeletePerson(Person person)
        {
            _context.Persons.Remove(person);
        }

        public async Task AddPet(Pet pet)
        {
            if(pet == null)
            {
                throw new ArgumentNullException(nameof(pet));
            }

            pet.PetId = Guid.NewGuid();
            await _context.Pets.AddAsync(pet);
        }

        public List<Pet> GetPets()
        {
            return _context.Pets.ToList();
        }

        public List<PetType> GetPetTypes()
        {
            return _context.PetTypes.ToList();
        }

        public async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync() >= 0;
            return result;
        }

        public List<BirthState> GetBirthStates()
        {
            return _context.BirthStates.OrderBy(x => x.Abbreviation).ToList();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
