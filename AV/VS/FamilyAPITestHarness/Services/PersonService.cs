using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAPITestHarness.Services
{
    public class PersonService : IPersonService
    {
        IConfigurationRoot _configurationRoot;
        string URIEndPoint, id = string.Empty;
        string uripath = string.Empty;

        // CTOR.
        public PersonService(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
            URIEndPoint = _configurationRoot.GetSection("FamilyAPI").GetSection("URI").Value;
        }

        public async Task AddPerson(string dataType)
        {
            uripath = GetURIPath(dataType);

            // TODO: Add record counter to MiddleName
            using (var httpClient = new HttpClient())
            {
                // TODO: Add method to return in memory data.
                string data = @"{
                    ""age"": 25,
                    ""city"": ""Atlanta"",
                    ""country"": ""USA"",
                    ""dateOfBirth"": ""1975 -01-11"",
                    ""firstName"": ""April"",
                    ""gender"": ""Female"",
                    ""lastName"": ""Showers"",
                    ""mIddleName"": ""May"",
                    ""stateId"": 51,
                    ""createdate"": """ + DateTime.Now.ToString() + @"""}";

                HttpContent httpContent = new StringContent(data, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.BaseAddress = new Uri(URIEndPoint);

                var response = await httpClient.PostAsync(uripath, httpContent);

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

        private string GetURIPath(string dataType, string objectKey = null)
        {
            switch (dataType)
            {
                case "Persons":
                    uripath = _configurationRoot.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value;
                    break;
                default:
                    break;
            }

            return uripath;
        }
    }
}
