using System.Threading.Tasks;

namespace FamilyAPITestHarness.Interfaces
{
    public interface IUtilties
    {
        Task<string> GetURIPath(string route, string objectKey = null);
    }
}
