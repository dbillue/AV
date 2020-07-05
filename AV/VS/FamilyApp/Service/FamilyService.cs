using FamilyApp.DBContext;
using FamilyApp.Model;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<BirthState>> GetBirthStatesAsync()
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

        public async Task AddPerson(Person person)
        {
            await _context.person.AddAsync(person);
        }

        public async Task UpdatePerson(Person person)
        {
            _context.person.Where(p => p.PersonId == person.PersonId).FirstOrDefault();
            await _context.SaveChangesAsync();
            //return person;
        }
    }
}
