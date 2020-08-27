using FamilyAPITestHarness.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FamilyAPITestHarness.Utils
{
    public class Utilities : IUtilties
    {
        private readonly IConfiguration _configuration;

        // Ctor.
        public Utilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetURIPath(string route, string objectKey = null)
        {
            switch (route)
            {
                case "AddPerson":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value;
                    break;
                case "QueryPerson":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value + "/" + objectKey;
                    break;
                case "QueryPersons":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value;
                    break;
                case "UpdatePerson":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value + "/" + objectKey;
                    break;
                case "DeletePerson":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value + "/" + objectKey;
                    break;
                case "AddPet":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Pet_Path").Value;
                    break;
                case "UpdatePet":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Pet_Path").Value + "/" + objectKey;
                    break;
                case "GetPet":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_GetPet_Path").Value + "?petId=" + objectKey;
                    break;
                case "GetPets":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_PetList_Path").Value;
                    break;
                case "DeletePet":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Pet_Path").Value + "/" + objectKey;
                    break;
                default:
                    break;
            }

            return await Task.FromResult(route);
        }
    }
}
