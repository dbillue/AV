using System.Threading.Tasks;

namespace FamilyAPITestHarness.Interfaces
{
    public interface IPersonController
    {
        Task TestAction();

        Task AddPerson();

        Task UpdatePerson();

        Task DeletePerson();
    }
}
