using FamilyApp.Model;
using FamilyApp.Service;
using FamilyApp.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyApp.Pages
{
    public partial class Family
    {
        //TODO: Add error handling everywhere and redirect page

        #region // Object creation
        List<BirthState> birthStateList;
        List<Person> personList;
        List<Pet> petList;
        List<PetTypes> petTypeList;
        Pet pet = new Pet();
        PetTypes petType = new PetTypes();
        Person person = new Person();
        Person objAddPerson = new Person();
        SeriLog_Logger seriLogger = new SeriLog_Logger();
        JsonUtils jsonUtils = new JsonUtils();
        #endregion

        #region // Fields and properties
        private string people = string.Empty;
        private string fullName = string.Empty;
        private bool showEditPerson = false;
        private bool showAddPerson = false;
        private bool showDeletePerson = false;
        private bool showAddPets = false;
        private bool showPets = false;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            try
            {
                objAddPerson.DateOfBirth = DateTime.Now;
                personList = await GetPersons();
            }
            catch (Exception ex)
            {
                seriLogger.WriteError(ex.Message);
            }
        }

        public async Task<List<Person>> GetPersons()
        {
            try
            {
                // Pull from local SQL Server db.
                //personList = await FamilyService.GetPeople();

                // Pull from REST API end point that queries Azure SQL Server db.
                people = await FamilyAPIService.GetPeopleRaw();
                personList = await jsonUtils.DeserializePeople(people);

                petList = await FamilyService.GetPets();
                petTypeList = await FamilyService.GetPetTypes();
                birthStateList = await FamilyService.GetBirthStates();

                foreach (var pet in petList)
                {
                    pet.petTypes = petTypeList;
                    pet.petType = GetPetType(pet);
                }

                foreach (var person in personList)
                {
                    person.Pets = GetPets(person);
                    person.birthState = birthStateList;
                    person.state = GetBirthState(person);
                }
            }
            catch (Exception ex)
            {
                seriLogger.WriteError(ex.Message);
            }

            return personList;
        }

        public List<Pet> GetPets(Person person)
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

        #region // Add person
        private void showAddPersonForm()
        {
            fullName = "";
            showAddPerson = true;
        }

        private async Task AddPerson()
        {
            objAddPerson.StateId = GetBirthStateId(objAddPerson);
            objAddPerson.CreateDate = DateTime.Now;
            Person newPerson = await FamilyService.AddPerson(objAddPerson);

            //TODO: Add additional field validation
            if (!string.IsNullOrEmpty(pet.Name))
            {
                await PetService.AddNewPet(newPerson, pet, petList, petType, "New");
            }

            objAddPerson.FirstName = string.Empty;
            objAddPerson.MIddleName = string.Empty;
            objAddPerson.LastName = string.Empty;
            objAddPerson.Gender = string.Empty;
            objAddPerson.Age = 0;
            objAddPerson.DateOfBirth = DateTime.Now;
            objAddPerson.City = string.Empty;
            objAddPerson.state = string.Empty;
            objAddPerson.Country = string.Empty;
            pet.Name = string.Empty;
            pet.NickName = string.Empty;
            petType.Type = string.Empty;

            showAddPerson = false;

            people = string.Empty;
            personList = await GetPersons();
        }
        #endregion

        #region // Edit person
        private void ShowEditPersonForm(Person personProfile)
        {
            try
            {
                showEditPerson = true;
                person = personProfile;
                fullName = personProfile.FirstName + " " + personProfile.LastName;
            }
            catch (Exception ex)
            {
                seriLogger.WriteError(ex.Message);
            }
        }

        private void ShowAddPetsForm()
        {
            if(showAddPets)
            {
                showAddPets = false;
            } else {
                showAddPets = true;

                pet.Name = string.Empty;
                pet.NickName = string.Empty;
                petType.Type = string.Empty;
            }
        }

        private async Task UpdatePerson()
        {
            person.StateId = GetBirthStateId(person);
            await FamilyService.UpdatePerson(person);

            //TODO: Add additional field validation
            if (!string.IsNullOrEmpty(pet.Name))
            {
                await PetService.AddNewPet(person, pet, petList, petType, "Existing");
            }

            showEditPerson = false;
            showAddPets = false;
            showPets = false;

            personList = await GetPersons();
        }
        #endregion

        #region // Delete person
        private void ShowDeletePersonForm(Person person)
        {
            fullName = person.FirstName + " " + person.LastName;
            showDeletePerson = true;
        }

        private async Task DeletePerson()
        { 
            //TODO: Fix DB concurrency issue 
            if(person.Pets.Count > 0)
            {
                await DeletePet(person);
            }

            await FamilyService.DeletePerson(person);
            showDeletePerson = false;
        }

        private async Task DeletePet(Person person)
        {
            foreach(var pet in petList)
            {
                if(person.PersonId == pet.PersonId)
                {
                    await FamilyService.DeletePet(pet);
                }
            }
        }
        #endregion

        private void ClosePopup(string formType)
        {
            switch (formType)
            {
                case "Add":
                    showAddPerson = false;
                    break;
                case "Edit":
                    showEditPerson = false;
                    showPets = false;
                    showAddPets = false;
                    break;
                case "Delete":
                    showDeletePerson = false;
                    break;
                default:
                    break;
            }
        }

        private void ShowPets()
        {
            if (showPets)
            {
                showPets = false;
            }
            else
            {
                showPets = true;
            }
        }

        private string GetBirthState(Person person)
        {
            string stateName = string.Empty;

            foreach (var state in person.birthState)
            {
                if (person.StateId == state.StateId)
                {
                    stateName = state.State;
                    break;
                }
            }

            return stateName;
        }

        private int GetBirthStateId(Person person)
        {
            int stateId = 1;
            foreach (var state in birthStateList)
            {
                if (person.state == state.State)
                {
                    stateId = state.StateId;
                    break;
                }
            }

            return stateId;
        }

        private string GetPetType(Pet pet)
        {
            string petType = string.Empty;
            foreach (var p in petList)
            {
                foreach (var petTypes in p.petTypes)
                {
                    if (pet.PetTypeId == petTypes.PetTypeId)
                    {
                        petType = petTypes.Type;
                        break;
                    }
                }
            }

            return petType;
        }
    }
}
