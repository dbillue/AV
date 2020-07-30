using FamilyApp.Model;
using FamilyApp.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FamilyApp.Service
{
    public class PetService : IPetService
    {
        SeriLog_Logger seriLogger = new SeriLog_Logger();
        private readonly FamilyService _familyService;

        public PetService(IServiceProvider serviceProvider)
        {
            _familyService = (FamilyService)serviceProvider.GetService<IFamilyService>();
        }

        public async Task<bool> AddNewPet(Person person, Pet pet, string petType)
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

        public int GetPetType(Pet pet, string petType)
        {
            int petTypeId = 0;

            switch (petType)
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

        public string GetPetType(Pet pet, List<Pet> petList, List<PetTypes> petTypeList)
        {
            string petType = string.Empty;

            try
            {
                foreach (var p in petList)
                {
                    foreach (var petTypes in petTypeList)
                    {
                        if (pet.PetTypeId == petTypes.PetTypeId)
                        {
                            petType = petTypes.Type;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                seriLogger.WriteError(ex.Message);
            }

            return petType;
        }
    }
}
