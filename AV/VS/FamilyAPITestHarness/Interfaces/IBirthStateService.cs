using System.Threading.Tasks;

namespace FamilyAPITestHarness.Interfaces
{
    public interface IBirthStateService
    {
        Task<string> GetStates(string route);
    }
}
