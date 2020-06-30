using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    interface IFamilyService
    {
        Task<List<BirthState>> GetBirthStatesAsync();

        List<BirthState> GetBirthStates();

        Task<List<Person>> GetPeople();

        Task<List<Pet>> GetPets();
    }
}
