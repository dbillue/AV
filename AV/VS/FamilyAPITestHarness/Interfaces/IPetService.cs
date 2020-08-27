using System.Threading.Tasks;

namespace FamilyAPITestHarness.Interfaces
{
    public interface IPetService
    {
        Task AddPet(string route, string data);

        Task<string> GetPet(string route, string objectKey);

        Task<string> GetPets(string route);

        Task<string> GetPetTypes(string route);

        Task UpdatePet(string route, string objectKey, string updateData);

        Task DeletePet(string route, string objectKey);
    }
}
