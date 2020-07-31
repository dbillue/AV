using FamilyApp.Model;
using System;
using System.Collections.Generic;

namespace FamilyApp.DTO
{
    public class PersonDTO
    {
        public Guid PersonId { get; set; }

        public string FirstName { get; set; }

        public string MIddleName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int StateId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreateDate { get; set; }

        public List<Pet> Pets;
        public List<BirthState> birthState;
        public string state;

        public PersonDTO()
        {
            Pets = new List<Pet>();
            birthState = new List<BirthState>();
            state = string.Empty;
        }
    }
}
