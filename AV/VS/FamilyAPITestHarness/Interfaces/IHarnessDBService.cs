using FamilyAPITestHarness.Entites;
using FamilyAPITestHarness.DTO;
using System.Collections.Generic;
using System.Linq;

namespace FamilyAPITestHarness.Services
{
    public interface IHarnessDBService
    {
        IQueryable<PersonGuidDTO> GetPersonIds();

        List<Person> GetPersons();
    }
}
