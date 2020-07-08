using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    interface IFamilyAPIService
    {
        Task<string> GetPeopleRaw();
    }
}
