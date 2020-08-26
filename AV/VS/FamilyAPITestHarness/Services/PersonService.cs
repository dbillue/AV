using FamilyAPITestHarness.Entites;
using FamilyAPITestHarness.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAPITestHarness.Services
{
    public class PersonService : IPersonService
    {
        IConfiguration _configuration;
        string URIEndPoint, id = string.Empty;
        string route = string.Empty;

        // CTOR.
        public PersonService(IConfiguration configuration)
        {
            _configuration = configuration;
            URIEndPoint = _configuration.GetSection("FamilyAPI").GetSection("URI").Value;
        }

        public async Task AddPerson(string route, string data)
        {
            route = GetURIPath(route);

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

        public void DeletePerson(string route, string objectKey)
        {
            route = GetURIPath(route, objectKey);

            using (var httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(objectKey, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.BaseAddress = new Uri(URIEndPoint);

                var response = httpClient.DeleteAsync(route).Result;
            }
        }

        public async Task UpdatePerson(string route, string objectKey, string updateData)
        {
            route = GetURIPath(route, objectKey);

            using (var httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(updateData, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.BaseAddress = new Uri(URIEndPoint);

                var response = await httpClient.PatchAsync(route, httpContent);
            }
        }

        // TODO: Complete call to QueryPerson()
        public void QueryPerson(Guid personId)
        {

        }

        // TODO: Complete call to QueryPersons()
        public void QueryPersons()
        {

        }

        // TODO: Add as extension / helper method
        private string GetURIPath(string route, string objectKey = null)
        {
            switch (route)
            {
                case "AddPerson":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value;
                    break;
                case "UpdatePerson":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value + "/" + objectKey;
                    break;
                case "DeletePerson":
                    route = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value + "/" + objectKey;
                    break;
                default:
                    break;
            }

            return route;
        }
    }
}
