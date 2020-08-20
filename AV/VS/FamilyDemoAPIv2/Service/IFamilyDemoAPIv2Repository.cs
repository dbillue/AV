using FamilyDemoAPIv2.Entities;
using FamilyDemoAPIv2.Helpers;
using FamilyDemoAPIv2.ResourceParameters;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FamilyDemoAPIv2.Service
{
    public interface IFamilyDemoAPIv2Repository
    {
        string Response();

        Task<bool> PersonExists(Guid personId);

        Task AddPerson(Person person);

        Task<Person> GetPerson(Guid personId);

        Task<PagedList<Person>> GetPersons(PersonResourceParameters authorsResourceParameters);

        Task<Person> UpdatePerson(Person person);

        Task DeletePerson(Person person);

        Task<bool> PetExists(Guid petId);

        Task<List<Pet>> GetPets();

        Task<Pet> GetPet(Guid petId);

        Task<List<PetType>> GetPetTypes();

        Task<Pet> UpdatePet(Pet pet);

        Task<List<BirthState>> GetBirthStates();

        Task DeletePet(Pet pet);

        Task AddPet(Pet pet);

        Task<bool> Save();
    }
}
