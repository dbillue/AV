using FamilyApp.DTO;
using FamilyApp.Extensions;
using FamilyApp.Model;
using FamilyApp.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyApp.Pages
{
    public partial class Family
    {
        #region // Object creation
        List<BirthState> birthStateList;
        List<Person> personList;
        List<Pet> petList;
        List<PetTypes> petTypeList;
        Pet pet = new Pet();
        PetDTO petDTO = new PetDTO();
        Person person = new Person();
        PersonDTO personDTO = new PersonDTO();
        SeriLog_Logger seriLogger = new SeriLog_Logger();
        JsonUtils jsonUtils = new JsonUtils();
        #endregion

        #region // Fields and properties
        private string people = string.Empty;
        private string birthStates = string.Empty;
        private string pets = string.Empty;
        private string pettypes = string.Empty;
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
                person.DateOfBirth = DateTime.Now;
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
                // personList = await FamilyService.GetPeople();

                // Pull from REST API end point that queries Azure SQL Server db.
                people = await FamilyAPIService.GetFamilyAPIData("persons");
                personList = await jsonUtils.DeserializePeople(people);
                birthStates = await FamilyAPIService.GetFamilyAPIData("states");
                birthStateList = await jsonUtils.DeserializeBirthStates(birthStates);

                pets = await FamilyAPIService.GetFamilyAPIData("pets");
                petList = await jsonUtils.DeserializePets(pets);
                pettypes = await FamilyAPIService.GetFamilyAPIData("pettypes");
                petTypeList = await jsonUtils.DeserializePetTypes(pettypes);
                petDTO.petTypes = petTypeList;

                foreach (var pet in petList)
                {
                    pet.petType = PetService.GetPetType(pet.PetTypeId, petTypeList);
                }

                foreach (var person in personList)
                {
                    person.Pets = PetService.GetPets(person, petList);
                    person.birthState = birthStateList;
                    person.state = BirthState.GetBirthState(person);
                }
            }
            catch (Exception ex)
            {
                seriLogger.WriteError(ex.Message);
            }

            return personList;
        }

        #region // Add person
        private void showAddPersonForm()
        {
            fullName = "";
            personDTO.DateOfBirth = DateTime.Now;
            showAddPerson = true;
        }

        private async Task AddPerson()
        {  
            //TODO: Add automapper for person
            person.FirstName = personDTO.FirstName;
            person.MIddleName = personDTO.MIddleName;
            person.LastName = personDTO.LastName;
            person.Gender = personDTO.Gender;
            person.Age = personDTO.Age;
            person.DateOfBirth = personDTO.DateOfBirth;
            person.City = personDTO.City;
            person.state = personDTO.state;
            person.Country = personDTO.Country;
            person.StateId = BirthState.GetBirthStateId(person, birthStateList);
            person.CreateDate = DateTime.Now;
            person.PersonId = new Guid();

            // Use FamilyAPI for adding person.
            string jsonPerson = jsonUtils.SerializePerson(person);
            bool response = await FamilyAPIService.PostFamilyAPIData("Person", jsonPerson);

            // Use EFCore for adding person.
            // await FamilyService.AddPerson(person);

            if (!string.IsNullOrEmpty(petDTO.Name) && !string.IsNullOrEmpty(petDTO.NickName) && !string.IsNullOrEmpty(petDTO.petType))
            {
                //TODO: Add Automapper for pet
                pet.Name = petDTO.Name;
                pet.NickName = petDTO.NickName;
                pet.petType = petDTO.petType;
                var petAdded = await PetService.AddNewPet(person, pet, petTypeList, pet.petType);
            }

            HelperExtensions.ClearObjectValues("personDTO", personDTO);
            HelperExtensions.ClearObjectValues("petDTO", null, petDTO);

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
                person = personProfile;
                fullName = person.FirstName + " " + person.LastName;
                HelperExtensions.ClearObjectValues("petDTO", null, petDTO);

                showEditPerson = true;
            }
            catch (Exception ex)
            {
                seriLogger.WriteError(ex.Message);
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

        private void ShowAddPetsForm()
        {
            if (showAddPets)
            {
                showAddPets = false;
            }
            else
            {
                showAddPets = true;
            }
        }

        private async Task UpdatePerson()
        {
            showEditPerson = false;
            showAddPets = false;
            showPets = false;

            if (!string.IsNullOrEmpty(petDTO.Name) && !string.IsNullOrEmpty(petDTO.NickName) && !string.IsNullOrEmpty(petDTO.petType))
            {
                pet.Name = petDTO.Name;
                pet.NickName = petDTO.NickName;
                pet.petType = petDTO.petType;
                var petAdded = await PetService.AddNewPet(person, pet, petTypeList, pet.petType);
            }

            person.StateId = BirthState.GetBirthStateId(person, birthStateList);
            await FamilyService.UpdatePerson(person);

            personList = await GetPersons();
        }

        private async Task DeletePet(Pet pet)
        {
            var petDeleted = await PetService.DeletePet(pet);

            petList.Remove(pet);
            person.Pets.Remove(pet);

            personList = await GetPersons();
        }
        #endregion

        #region // Delete person
        private void ShowDeletePersonForm(Person personProfile)
        {
            person = personProfile;
            fullName = person.FirstName + " " + person.LastName;
            showDeletePerson = true;
        }

        private async Task DeletePerson()
        {
            if (person.Pets.Count > 0)
            {
                await DeletePet(person);
            }

            await FamilyService.DeletePerson(person);
            showDeletePerson = false;

            personList = await GetPersons();
        }

        private async Task DeletePet(Person person)
        {
            foreach (var pet in petList)
            {
                if (person.PersonId == pet.PersonId)
                {
                    var petDeleted = await PetService.DeletePet(pet);
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

        private void updateDOB(string action)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;

            switch (action)
            {
                case "add":
                    personDTO.DateOfBirth = dt.AddYears(-personDTO.Age);
                    break;
                case "edit":
                    person.DateOfBirth = dt.AddYears(-person.Age);
                    break;
                default:
                    break;
            }
        }
    }
}
