using FamilyAPITestHarness.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FamilyAPITestHarness.Services
{
    public class BirthStateService : IBirthStateService
    {
        IConfiguration _configuration;
        IHarnessDBService _harnessDBService;
        IUtilties _utilties;
        string URIEndPoint, id = string.Empty;
        string route = string.Empty;

        // CTOR.
        public BirthStateService(IConfiguration configuration, IHarnessDBService harnessDBService, IUtilties utilties)
        {
            _configuration = configuration;
            _harnessDBService = harnessDBService;
            _utilties = utilties;
            URIEndPoint = _configuration.GetSection("FamilyAPI").GetSection("URI").Value;
        }

        public async Task<string> GetStates(string route)
        {
            route = await _utilties.GetURIPath(route);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URIEndPoint);
                return await httpClient.GetStringAsync(route);
            }
        }
    }
}
