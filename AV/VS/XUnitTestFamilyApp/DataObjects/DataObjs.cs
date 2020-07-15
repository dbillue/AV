using System;
using System.Collections.Generic;
using System.Text;
using FamilyApp.Model;

namespace XUnitTestFamilyApp.DataObjects
{
    public class DataObjs
    {
        public Person GetPerson()
        { 
            Person person = new Person
            {
                PersonId = Guid.NewGuid()
            };

            return person;
        }

        public Pet GetPet()
        { 
            Pet pet = new Pet
            {
                Name = "Hay",
                NickName = "HayHay",
                PetTypeId = (int)PetType.Cat,
                PersonId = Guid.Parse("e2216454-153a-4a3a-9d13-9c2a4abe1e0d"),
                CreateDate = DateTime.Now,
            };

            return pet;
        }

        public PetTypes GetPetTypes(string petTypeName)
        {
            int petTypeId = 0;
            string type = string.Empty;

            switch (petTypeName)
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

            PetTypes petType = new PetTypes
            {
                PetTypeId = petTypeId,
                Type = petTypeName
            };

            return petType;
        }
    }
}
