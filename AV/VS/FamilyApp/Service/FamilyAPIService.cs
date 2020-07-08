﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using FamilyApp.Model;
using Microsoft.Extensions.Configuration;

namespace FamilyApp.Service
{
    public class FamilyAPIService : IFamilyAPIService
    {
        IConfiguration configuration;
        string URIEndPoint, URI_Path = string.Empty;

        public FamilyAPIService(IConfiguration _configuration) 
        {
            configuration = _configuration;
            URIEndPoint = configuration.GetSection("FamilyAPI").GetSection("URI").Value;
            URI_Path = configuration.GetSection("FamilyAPI").GetSection("URI_Path").Value;
        }

        public async Task<string> GetPeopleRaw()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URIEndPoint);
                return await httpClient.GetStringAsync(URI_Path);
            }
        }
    }
}
