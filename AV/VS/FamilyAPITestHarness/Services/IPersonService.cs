using System.Threading.Tasks;

namespace FamilyAPITestHarness.Services
{
    public interface IPersonService
    {
        Task AddPerson(string dataType);
    }
}
