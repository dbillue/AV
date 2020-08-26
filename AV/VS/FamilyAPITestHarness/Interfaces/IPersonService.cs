using FamilyAPITestHarness.Entites;
using System;
using System.Threading.Tasks;

namespace FamilyAPITestHarness.Interfaces
{
    public interface IPersonService
    {
        Task AddPerson(string route, string data);

        void DeletePerson(string route, string personId);

        Task UpdatePerson(string route, string objectKey, string updateData);

        void QueryPerson(Guid personId);

        void QueryPersons();
    }
}
