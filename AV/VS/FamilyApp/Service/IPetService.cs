using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyApp.Model;

namespace FamilyApp.Service
{
    public interface IPetService
    {
        Task<bool> AddNewPet(Person person, Pet pet, List<PetTypes> petTypeList, string petType);

        Task<bool> DeletePet(Pet pet);

        int GetPetType(List<PetTypes> petTypeList, string petType);

        string GetPetType(int petTypeId, List<PetTypes> petTypeList);

        List<Pet> GetPets(Person person, List<Pet> petList);
    }
}
