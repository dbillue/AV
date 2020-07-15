using FamilyApp.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FamilyApp.Service
{
    public class PetService : IPetService
    {
        private readonly FamilyService _familyService;

        public PetService(IServiceProvider serviceProvider)
        {
            _familyService = (FamilyService)serviceProvider.GetService<IFamilyService>();
        }

        public async Task<bool> AddNewPet(Person person, Pet pet, PetTypes petType)
        {
            // Assign default properties.
            pet.PersonId = person.PersonId;
            pet.CreateDate = DateTime.Now;
            pet.PetTypeId = GetPetType(pet, petType);
            await _familyService.AddPet(pet);
            return true;
        }

        public async Task<bool> DeletePet(Pet pet)
        {
            await _familyService.DeletePet(pet);
            return true;
        }

        public int GetPetType(Pet pet, PetTypes petType)
        {
            int petTypeId = 0;

            switch (pet.petType)
            {
                case "Cat":
                    petTypeId = 1;
                    break;
                case "Dog":
                    petTypeId = 2;
                    break;
                case "Reptile":
                    petTypeId = 3;
                    break;
                default:
                    break;
            }

            return petTypeId;
        }
    }
}
