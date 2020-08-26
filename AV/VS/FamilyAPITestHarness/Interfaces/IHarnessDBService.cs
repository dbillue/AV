using FamilyAPITestHarness.Entites;
using FamilyAPITestHarness.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FamilyAPITestHarness.Services
{
    public interface IHarnessDBService
    {
        Task<IQueryable<PersonGuidDTO>> GetPersonIds();

        Task<List<Person>> GetPersons();
    }
}
