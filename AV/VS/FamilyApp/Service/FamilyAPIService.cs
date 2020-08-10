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
        string URIEndPoint, id = string.Empty;
        string uripath = string.Empty;

        public FamilyAPIService(IConfiguration _configuration)
        {
            configuration = _configuration;
            URIEndPoint = configuration.GetSection("FamilyAPI").GetSection("URI").Value;
        }

        public async Task<string> GetFamilyAPIData(string dataType)
        {
            switch (dataType)
            {
                case "persons":
                    uripath = configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value;
                    break;
                case "states":
                    uripath = configuration.GetSection("FamilyAPI").GetSection("URI_BirthState_Path").Value;
                    break;
                case "pets":
                    uripath = configuration.GetSection("FamilyAPI").GetSection("URI_PetList_Path").Value;
                    break;
                case "pettypes":
                    uripath = configuration.GetSection("FamilyAPI").GetSection("URI_PetTypes_Path").Value;
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
                    uripath = configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value;
                    break;
                case "Pet":
                    uripath = configuration.GetSection("FamilyAPI").GetSection("URI_AddPet_Path").Value;
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

        public async Task<bool> DeleteFamilyAPIData(string dataType, string objectKey)
        {
            switch(dataType)
            {
                case "Pet":
                    uripath = configuration.GetSection("FamilyAPI").GetSection("URI_DeletePet_Path").Value + "/" + objectKey;
                    break;
                default:
                    break;
            }

            using (var httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(objectKey, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.BaseAddress = new Uri(URIEndPoint);

                var response = await httpClient.DeleteAsync(uripath);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
