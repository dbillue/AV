using FamilyAPITestHarness.DBContext;
using FamilyAPITestHarness.Model;
using FamilyAPITestHarness.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAPITestHarness.Services
{
    public class HarnessDBService : IHarnessDBService
    {
        private readonly HarnessDBContext _dbContext;

        // Ctor.
        public HarnessDBService(HarnessDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Query for list all Person.PersonId values
        public async Task<List<PersonGuid>> GetPersonIds()
        {
            var PersonIds = from person in _dbContext.Person
                            select new PersonGuid
                            {
                                personId = (Guid)person.PersonId
                            };

            return await PersonIds.ToListAsync();
        }

        // Query for list all Pet.PetId values
        public async Task<List<PetGuid>> GetPetIds()
        {
            var PetIds = from pet in _dbContext.Pet
                            select new PetGuid
                            {
                                petId = (Guid)pet.PetId
                            };

            return await PetIds.ToListAsync();
        }

        // Return Person model
        public async Task<List<Person>> GetPersons()
        {
            var lstPerson = await _dbContext.Person.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync<Person>();
            return lstPerson;
        }
    }
}
