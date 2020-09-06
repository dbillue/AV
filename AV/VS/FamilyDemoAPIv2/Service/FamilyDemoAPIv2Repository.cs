using FamilyDemoAPIv2.DBContext;
using FamilyDemoAPIv2.Entities;
using FamilyDemoAPIv2.Helpers;
using FamilyDemoAPIv2.ResourceParameters;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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

        public async Task<bool> PersonExists(Guid personId)
        {
            if (personId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(personId));
            }

            return await _context.Persons.AnyAsync(a => a.PersonId == personId);
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
            await Save();
        }

        public async Task<Person> GetPerson(Guid personId)
        {
            if (personId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(personId));
            }

            return await _context.Persons
              .Where(c => c.PersonId == personId).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Person>> GetPersons(PersonResourceParameters authorsResourceParameters)
        {
            if (authorsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(authorsResourceParameters));
            }

            var collection = await _context.Persons.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync<Person>();

            // Paging.
            return PagedList<Person>.Create(collection.AsQueryable<Person>(),
                authorsResourceParameters.PageNumber,
                authorsResourceParameters.PageSize);
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            var _person = await _context.Persons
              .Where(a => a.PersonId == person.PersonId).FirstOrDefaultAsync();
            await Save();
            return _person;
        }

        public async Task DeletePerson(Person person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PetExists(Guid petId)
        {
            if (petId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(petId));
            }

            return await _context.Pets.AnyAsync(a => a.PetId == petId);
        }

        public async Task<Pet> GetPet(Guid petId)
        {
            if (petId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(petId));
            }

            return await _context.Pets
              .Where(c => c.PetId == petId).FirstOrDefaultAsync();
        }

        public async Task AddPet(Pet pet)
        {
            if(pet == null)
            {
                throw new ArgumentNullException(nameof(pet));
            }

            pet.PetId = Guid.NewGuid();
            await _context.Pets.AddAsync(pet);
            await Save();
        }

        public async Task<List<Pet>> GetPets()
        {
            return await _context.Pets.ToListAsync<Pet>();
        }

        public async Task<List<PetType>> GetPetTypes()
        {
            return await _context.PetTypes.ToListAsync<PetType>();
        }

        public async Task<Pet> UpdatePet(Pet pet)
        {
            var _pet = await _context.Pets
                .Where(x => x.PetId == pet.PetId).FirstOrDefaultAsync();
            await Save();
            return _pet;
        }

        public async Task DeletePet(Pet pet)
        {
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync() >= 0;
            return result;
        }

        public async Task<List<BirthState>> GetBirthStates()
        {
            return await _context.BirthStates.OrderBy(x => x.Abbreviation).ToListAsync<BirthState>();
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
