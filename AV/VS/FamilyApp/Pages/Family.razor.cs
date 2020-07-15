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
        Pet addPetPerson = new Pet();
        Pet addPetPersonUpdate = new Pet();
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

                addPetPersonUpdate.petTypes = petTypeList;
                addPetPerson.petTypes = petTypeList;

                foreach (var pet in petList)
                { 
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
            await FamilyService.AddPerson(objAddPerson);

            //TODO: Add additional field validation
            if (!string.IsNullOrEmpty(addPetPerson.Name))
            {
                var petAdded = await PetService.AddNewPet(objAddPerson, addPetPerson, petType);
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
            }
        }

        private async Task UpdatePerson()
        {
            //TODO: Add additional field validation
            if (!string.IsNullOrEmpty(addPetPersonUpdate.Name))
            {
                var petAdded = await PetService.AddNewPet(person, addPetPersonUpdate, petType);
            }

            person.StateId = GetBirthStateId(person);
            await FamilyService.UpdatePerson(person);

            showEditPerson = false;
            showAddPets = false;
            showPets = false;

            personList = await GetPersons();

            //addPetPersonUpdate.Name = string.Empty;
            //addPetPersonUpdate.NickName = string.Empty;
            //addPetPersonUpdate.PersonId = Guid.Empty;
            //addPetPersonUpdate.PetTypeId = 0;
        }

        private async Task DeletePet(Pet pet)
        {
            var petDeleted = await PetService.DeletePet(pet);

            ClosePopup("Edit");

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

            personList = await GetPersons();
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
                    pet.Name = string.Empty;
                    pet.NickName = string.Empty;
                    petType.Type = string.Empty;
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

            try
            {
                foreach (var state in person.birthState)
                {
                    if (person.StateId == state.StateId)
                    {
                        stateName = state.State;
                        break;
                    }
                }
            } catch (Exception ex) {
                seriLogger.WriteError(ex.Message);
            }

            return stateName;
        }

        private int GetBirthStateId(Person person)
        {
            int stateId = 1;

            try
            {
                foreach (var state in birthStateList)
                {
                    if (person.state == state.State)
                    {
                        stateId = state.StateId;
                        break;
                    }
                }
            } catch (Exception ex) {
                seriLogger.WriteError(ex.Message);
            }

            return stateId;
        }

        private string GetPetType(Pet pet)
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
            } catch (Exception ex) { 
                seriLogger.WriteError(ex.Message);
            }

            return petType;
        }
    }
}
