using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FamilyApp.Service
{
    public class FamilyAPIService : IFamilyAPIService
    {
        IConfiguration configuration;
        string URIEndPoint, URI_Persons_Path, URI_BirthState_Path = string.Empty;

        public FamilyAPIService(IConfiguration _configuration)
        {
            configuration = _configuration;
            URIEndPoint = configuration.GetSection("FamilyAPI").GetSection("URI").Value;
            URI_Persons_Path = configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value;
            URI_BirthState_Path = configuration.GetSection("FamilyAPI").GetSection("URI_BirthState_Path").Value;
        }

        public async Task<string> CallFamilyAPI(string dataType)
        {
            string uripath = string.Empty;

            switch (dataType)
            {
                case "persons":
                    uripath = URI_Persons_Path;
                    break;
                case "states":
                    uripath = URI_BirthState_Path;
                    break;
                default:
                    break;
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URIEndPoint);
                return await httpClient.GetStringAsync(uripath);
            }
        }
    }
}
