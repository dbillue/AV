using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    public class FamilyAPIService : IFamilyAPIService
    {
        IConfiguration configuration;
        string URIEndPoint, URI_Persons_Path, URI_BirthState_Path, URI_PetList_Path, URI_PetTypes_Path, URI_AddPet_Path, id = string.Empty;
        string uripath = string.Empty;

        public FamilyAPIService(IConfiguration _configuration)
        {
            configuration = _configuration;
            URIEndPoint = configuration.GetSection("FamilyAPI").GetSection("URI_Dev").Value;
            URI_Persons_Path = configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value;
            URI_BirthState_Path = configuration.GetSection("FamilyAPI").GetSection("URI_BirthState_Path").Value;
            URI_PetList_Path = configuration.GetSection("FamilyAPI").GetSection("URI_PetList_Path").Value;
            URI_PetTypes_Path = configuration.GetSection("FamilyAPI").GetSection("URI_PetTypes_Path").Value;
            URI_AddPet_Path = configuration.GetSection("FamilyAPI").GetSection("URI_AddPet_Path").Value;
        }

        public async Task<string> GetFamilyAPIData(string dataType)
        {
            switch (dataType)
            {
                case "persons":
                    uripath = URI_Persons_Path;
                    break;
                case "states":
                    uripath = URI_BirthState_Path;
                    break;
                case "pets":
                    uripath = URI_PetList_Path;
                    break;
                case "pettypes":
                    uripath = URI_PetTypes_Path;
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

        public async Task<string> PostFamilyAPIData(string dataType, string data)
        {
            switch (dataType)
            {
                case "Person":
                    uripath = URI_Persons_Path;
                    break;
                case "Pet":
                    uripath = URI_AddPet_Path;
                    break;
                default:
                    break;
            }

            using (var httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(data, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.BaseAddress = new Uri(URIEndPoint);

                var response = await httpClient.PostAsync(uripath, httpContent);

                // Extract GUID of newly created object.
                var locationHeader = response.Headers.GetValues("Location");

                foreach (string value in locationHeader)
                {
                    id = value.Substring(value.Length - 36, 36);
                }

                if (response.IsSuccessStatusCode)
                {
                    return id;
                }

                return "error";
            }
        }
    }
}
