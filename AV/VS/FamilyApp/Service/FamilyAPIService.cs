using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    public class FamilyAPIService : IFamilyAPIService
    {
        public FamilyAPIService() { }

        public async Task<string> GetPeopleRaw()
        {
            using (var httpClient = new HttpClient())
            {
                //TODO: Move to startup and add URI to JSON config
                httpClient.BaseAddress = new Uri("http://192.168.0.5:15001/");
                return await httpClient.GetStringAsync("/api/persons");
            }
        }
    }
}
