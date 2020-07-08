using FamilyApp.DBContext;
using FamilyApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp.Service
{
    public class FamilyService : IFamilyService
    {
        private readonly FamilyAppContext _context;

        //ctor.
        public FamilyService(FamilyAppContext context)
        {
            _context = context;
        }

        public async Task<List<BirthState>> GetBirthStates()
        {
            return await _context.birthState.OrderBy(x => x.StateId).ToListAsync();
        }

        public async Task<List<Person>> GetPeople()
        {
            return await _context.person.OrderBy(x => x.LastName).ToListAsync();
        }

        public async Task<List<Pet>> GetPets()
        {
            return await _context.pet.ToListAsync();
        }

        public async Task<List<PetTypes>> GetPetTypes()
        {
            return await _context.petType.ToListAsync();
        }

        public async Task AddPet(Pet pet)
        {
            pet.PetId = new Guid();
            _context.pet.Add(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePet(Pet pet)
        {
            _context.Remove(pet);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> AddPerson(Person person)
        {
            person.PersonId = new Guid();
            _context.person.Add(person);
            await _context.SaveChangesAsync();

            return _context.person.Where(p => person.PersonId == p.PersonId).FirstOrDefault();
        }

        public async Task UpdatePerson(Person person)
        {
            _context.person.Where(p => p.PersonId == person.PersonId).FirstOrDefault();
            await _context.SaveChangesAsync();
        }

        public async Task DeletePerson(Person person)
        {
            _context.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
