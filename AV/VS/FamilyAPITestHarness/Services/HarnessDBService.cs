using FamilyAPITestHarness.DBContext;
using FamilyAPITestHarness.DTO;
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

        // Query for list all Person.personId values
        public async Task<IQueryable<PersonGuidDTO>> GetPersonIds()
        {
            var PersonIds = from person in _dbContext.Person
                            select new PersonGuidDTO
                            {
                                personId = (Guid)person.PersonId
                            };

            return (IQueryable<PersonGuidDTO>)await PersonIds.ToListAsync();
        }

        public async Task<List<Person>> GetPersons()
        {
            var lstPerson = await _dbContext.Person.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync<Person>();
            return lstPerson;
        }
    }
}
