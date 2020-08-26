using FamilyAPITestHarness.DBContext;
using FamilyAPITestHarness.Entites;
using FamilyAPITestHarness.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IQueryable<PersonGuidDTO> GetPersonIds()
        {
            var PersonIds = from person in _dbContext.Person
                            select new PersonGuidDTO
                            {
                                personId = (Guid)person.PersonId
                            };

            return PersonIds.AsQueryable<PersonGuidDTO>();
        }

        public List<Person> GetPersons()
        {
            var lstPerson = _dbContext.Person.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList<Person>();
            return lstPerson;
        }
    }
}
