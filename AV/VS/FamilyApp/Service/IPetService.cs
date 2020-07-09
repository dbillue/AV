using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    interface IPetService
    {
        Task AddNewPet(Person person, Pet pet, PetTypes petType);

        Task DeletePet(Pet pet);

        int GetPetType(Pet pet, PetTypes petType);
    }
}
