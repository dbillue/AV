using FamilyApp.DBContext;
using FamilyApp.Model;
using FamilyApp.Pages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyApp.Service
{
    public class PetService : IPetService
    {
        private readonly FamilyService _familyService;

        public PetService(IServiceProvider serviceProvider) 
        {
            _familyService = (FamilyService)serviceProvider.GetService<IFamilyService>();
        }

        public async Task AddNewPet(Person person, Pet pet, List<Pet> petList, PetTypes petType, string personType)
        {
            // Assign default properties.
            pet.PersonId = person.PersonId;
            pet.CreateDate = DateTime.Now;
            pet.PetTypeId = GetPetType(pet, petType);

            switch (personType)
            {
                case "New":
                    await _familyService.AddPet(pet);
                    break;
                case "Existing":
                    foreach (var p in petList)
                    {
                        if (string.IsNullOrEmpty(p.PersonId.ToString()))
                        {
                            await _familyService.AddPet(pet);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public int GetPetType(Pet pet, PetTypes petType)
        {
            int petTypeId = 0;

            switch (petType.Type)
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
