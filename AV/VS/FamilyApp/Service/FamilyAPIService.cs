﻿using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FamilyApp.Service
{
    public class FamilyAPIService : IFamilyAPIService
    {
        IConfiguration _configuration;
        string URIEndPoint, id = string.Empty;
        string uripath = string.Empty;

        public FamilyAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
            URIEndPoint = _configuration.GetSection("FamilyAPI").GetSection("URI").Value;
        }

        public async Task<string> GetFamilyAPIData(string dataType)
        {
            uripath = GetURIPath(dataType);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URIEndPoint);
                return await httpClient.GetStringAsync(uripath);
            }
        }

        public async Task<string> PostFamilyAPIData(string dataType, string data)
        {
            uripath = GetURIPath(dataType);

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

        public async Task<string> PatchFamilyAPIData(string dataType, string objectkey, string data)
        {
            uripath = GetURIPath(dataType, objectkey);

            using (var httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(data, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.BaseAddress = new Uri(URIEndPoint);

                var response = await httpClient.PatchAsync(uripath, httpContent);

                return "";
            }
        }

        public async Task<bool> DeleteFamilyAPIData(string dataType, string objectKey)
        {
            uripath = GetURIPath(dataType, objectKey);

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

        private string GetURIPath(string dataType, string objectKey = null)
        {
            switch (dataType)
            {
                case "persons":
                    uripath = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value;
                    break;
                case "deleteperson":
                    uripath = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value + "/" + objectKey;
                    break;
                case "patchperson":
                    uripath = _configuration.GetSection("FamilyAPI").GetSection("URI_Persons_Path").Value + "/" + objectKey;
                    break;
                case "states":
                    uripath = _configuration.GetSection("FamilyAPI").GetSection("URI_BirthState_Path").Value;
                    break;
                case "pet":
                    uripath = _configuration.GetSection("FamilyAPI").GetSection("URI_Pet_Path").Value + "/" + objectKey;
                    break;
                case "patchpet":
                    uripath = _configuration.GetSection("FamilyAPI").GetSection("URI_Pet_Path").Value + "/" + objectKey;
                    break;
                case "pets":
                    uripath = _configuration.GetSection("FamilyAPI").GetSection("URI_Pet_Path").Value;
                    break;
                case "pettypes":
                    uripath = _configuration.GetSection("FamilyAPI").GetSection("URI_PetTypes_Path").Value;
                    break;
                default:
                    break;
            }

            return uripath;
        }
    }
}
