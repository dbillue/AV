using FamilyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp.Service
{
    interface IBirthState
    {
        string GetBirthState(Person person);

        int GetBirthStateId(Person person, List<Model.BirthState> birthStateList);
    }
}
