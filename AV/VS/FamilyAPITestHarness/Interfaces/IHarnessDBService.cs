using FamilyAPITestHarness.Entites;
using FamilyAPITestHarness.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FamilyAPITestHarness.Services
{
    public interface IHarnessDBService
    {
        Task<List<PersonGuid>> GetPersonIds();

        Task<List<PetGuid>> GetPetIds();

        Task<List<Person>> GetPersons();
    }
}
