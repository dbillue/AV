using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    public interface IFamilyAPIService
    {
        Task<string> GetFamilyAPIData(string dataType);

        Task<bool> PostFamilyAPIData(string dataType, string data);
    }
}
