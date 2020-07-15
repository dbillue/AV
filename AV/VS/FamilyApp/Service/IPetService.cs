using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    public interface IPetService
    {
        Task<bool> AddNewPet(Person person, Pet pet, PetTypes petType);

        Task DeletePet(Pet pet);

        int GetPetType(Pet pet, PetTypes petType);
    }
}
