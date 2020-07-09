using FamilyApp.DBContext;
using FamilyApp.Model;
using FamilyApp.Pages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FamilyApp.Service
{
    public class PetService : IPetService
    {
        private readonly FamilyService _familyService;

        public PetService(IServiceProvider serviceProvider)
        {
            _familyService = (FamilyService)serviceProvider.GetService<IFamilyService>();
        }

        public async Task AddNewPet(Person person, Pet pet, PetTypes petType)
        {
            // Assign default properties.
            pet.PersonId = person.PersonId;
            pet.CreateDate = DateTime.Now;
            pet.PetTypeId = GetPetType(pet, petType);
            await _familyService.AddPet(pet);
        }

        public async Task DeletePet(Pet pet)
        {
            await _familyService.DeletePet(pet);
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
