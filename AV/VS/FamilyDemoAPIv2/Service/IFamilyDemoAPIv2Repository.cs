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

        bool PersonExists(Guid personId);

        Task AddPerson(Person person);

        Person GetPerson(Guid personId);

        PagedList<Person> GetPersons(PersonResourceParameters authorsResourceParameters);

        Person UpdatePerson(Person person);

        List<Pet> GetPets();

        List<PetType> GetPetTypes();

        List<BirthState> GetBirthStates();

        void DeletePerson(Person person);

        Task<bool> Save();
    }
}
