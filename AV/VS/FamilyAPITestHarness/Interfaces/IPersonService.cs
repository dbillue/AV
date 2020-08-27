using FamilyAPITestHarness.Entites;
using System;
using System.Threading.Tasks;

namespace FamilyAPITestHarness.Interfaces
{
    public interface IPersonService
    {
        Task AddPerson(string route, string data);

        Task DeletePerson(string route, string objectKey);

        Task UpdatePerson(string route, string objectKey, string updateData);

        Task<string> GetPerson(string route, string objectKey);

        Task<string> GetPersons(string route);
    }
}
