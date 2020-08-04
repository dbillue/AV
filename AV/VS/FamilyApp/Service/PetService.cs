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
        JsonUtils jsonUtils;
        SeriLog_Logger seriLogger = new SeriLog_Logger();
        private readonly FamilyService _familyService;
        private readonly FamilyAPIService _familyAPIService;

        public PetService(IServiceProvider serviceProvider)
        {
            _familyService = (FamilyService)serviceProvider.GetService<IFamilyService>();
            _familyAPIService = (FamilyAPIService)serviceProvider.GetService<IFamilyAPIService>();
        }

        public async Task<bool> AddNewPet(Person person, Pet pet, List<PetTypes> petTypeList, string petType)
        {
            // Assign default properties.
            pet.PersonId = person.PersonId;
            pet.CreateDate = DateTime.Now;
            // TODO: Determine if method call is needed
            pet.PetTypeId = GetPetType(petTypeList, petType);

            // Use FamilyAPI for adding pet.
            jsonUtils = new JsonUtils();
            string jsonPet = jsonUtils.SerializePet(pet);
            bool response = await _familyAPIService.PostFamilyAPIData("Pet", jsonPet);

            // Use EFCore for adding person.
            //await _familyService.AddPet(pet);
            return true;
        }

        public async Task<bool> DeletePet(Pet pet)
        {
            await _familyService.DeletePet(pet);
            return true;
        }

        public int GetPetType(List<PetTypes> petTypeList, string petTypeName)
        {
            int petTypeId = 0;

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
            string petTypeName = string.Empty;

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
