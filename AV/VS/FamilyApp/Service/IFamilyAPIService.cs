using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    public interface IFamilyAPIService
    {
        Task<string> CallFamilyAPI(string dataType);
    }
}
