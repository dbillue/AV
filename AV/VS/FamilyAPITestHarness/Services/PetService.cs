using FamilyAPITestHarness.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAPITestHarness.Services
{
    public class PetService : IPetService
    {
        IConfiguration _configuration;
        IUtilties _utilties;
        string URIEndPoint, id = string.Empty;
        string route = string.Empty;

        // CTOR.
        public PetService(IConfiguration configuration, IUtilties utilties)
        {
            _configuration = configuration;
            _utilties = utilties;
            URIEndPoint = _configuration.GetSection("FamilyAPI").GetSection("URI").Value;
        }

        public async Task AddPet(string route, string data)
        {
            route = await _utilties.GetURIPath(route);

            using (var httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(data, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.BaseAddress = new Uri(URIEndPoint);

                var response = await httpClient.PostAsync(route, httpContent);

                #region // Response header parse
                // Extract GUID of newly created object.
                //var locationHeader = response.Headers.GetValues("Location");

                //foreach (string value in locationHeader)
                //{
                //    id = value.Substring(value.Length - 36, 36);
                //}

                //Console.WriteLine("Id {0}", id);

                //if (response.IsSuccessStatusCode)
                //{
                //    return id;
                //}
                #endregion
            }
        }

        public async Task<string> GetPet(string route, string objectKey)
        {
            route = await _utilties.GetURIPath(route, objectKey);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URIEndPoint);
                return await httpClient.GetStringAsync(route);
            }
        }

        public async Task<string> GetPets(string route)
        {
            route = await _utilties.GetURIPath(route);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URIEndPoint);
                return await httpClient.GetStringAsync(route);
            }
        }

        public async Task UpdatePet(string route, string objectKey, string updateData)
        {
            route = await _utilties.GetURIPath(route, objectKey);

            using (var httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(updateData, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.BaseAddress = new Uri(URIEndPoint);

                var response = await httpClient.PatchAsync(route, httpContent);
            }
        }

        public async Task<string> GetPetTypes(string route)
        {
            route = await _utilties.GetURIPath(route);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URIEndPoint);
                return await httpClient.GetStringAsync(route);
            }
        }

        public async Task DeletePet(string route, string objectKey)
        {
            route = await _utilties.GetURIPath(route, objectKey);

            using (var httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(objectKey, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.BaseAddress = new Uri(URIEndPoint);

                var response = await httpClient.DeleteAsync(route);
            }
        }
    }
}
