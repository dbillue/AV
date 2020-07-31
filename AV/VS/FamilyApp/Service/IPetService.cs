using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    public interface IPetService
    {
        Task<bool> AddNewPet(Person person, Pet pet, string petType);

        Task<bool> DeletePet(Pet pet);

        int GetPetType(Pet pet, string petType);

        string GetPetType(int petTypeId, List<PetTypes> petTypeList);

        List<Pet> GetPets(Person person, List<Pet> petList);
    }
}
