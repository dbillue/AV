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
        string jsonPet = string.Empty;
        string id = string.Empty;
        string petTypeName = string.Empty;
        int petTypeId = 0;
        bool deleted = false;

        JsonUtils jsonUtils;
        SeriLog_Logger seriLogger = new SeriLog_Logger();
        private readonly FamilyService _familyService;
        private readonly FamilyAPIService _familyAPIService;

        public PetService(IServiceProvider serviceProvider)
        {
            _familyService = (FamilyService)serviceProvider.GetService<IFamilyService>();
            _familyAPIService = (FamilyAPIService)serviceProvider.GetService<IFamilyAPIService>();
        }
        
        public async Task<bool> AddNewPet(Pet pet, List<PetTypes> petTypeList, string petType)
        {
            // Assign default properties.
            pet.CreateDate = DateTime.Now;
            pet.PetTypeId = GetPetType(petTypeList, petType);

            // Use FamilyAPI for adding pet.
            jsonUtils = new JsonUtils();
            jsonPet = jsonUtils.SerializeObj<Pet>(ref pet);
            id = await _familyAPIService.PostFamilyAPIData("Pet", jsonPet);

            // Use EFCore for adding pet.
            // await _familyService.AddPet(pet);
            return true;
        }

        public async Task<bool> DeletePet(Pet pet)
        {
            // Use FamilyAPI for deleting pet.
            deleted = await _familyAPIService.DeleteFamilyAPIData("Pet", pet.PetId.ToString());

            // Use EFCore for deleting pet.
            // await _familyService.DeletePet(pet);
            return deleted;
        }

        public int GetPetType(List<PetTypes> petTypeList, string petTypeName)
        {
            foreach(var petType in petTypeList)
            {
                if(petTypeName == petType.Type)
                {
                    petTypeId = petType.PetTypeId;
                }
            }

            return petTypeId;
        }

        public string GetPetType(int petTypeId, List<PetTypes> petTypeList)
        {
            try
            {
                foreach (var petType in petTypeList)
                {
                    if (petTypeId == petType.PetTypeId)
                    {
                        petTypeName = petType.Type;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                seriLogger.WriteError(ex.Message);
            }

            return petTypeName;
        }

        public List<Pet> GetPets(Person person, List<Pet> petList)
        {
            foreach (var pet in petList)
            {
                if (person.PersonId == pet.PersonId)
                {
                    person.Pets.Add(pet);
                }
            }

            return person.Pets;
        }
    }
}
