using FamilyApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp.Extensions
{
    public static class HelperExtensions
    {
        public static void ClearObjectValues(string objectType, PersonDTO personDTO = null, PetDTO petDTO = null)
        {
            switch (objectType)
            {
                case "personDTO":
                    personDTO.FirstName = string.Empty;
                    personDTO.LastName = string.Empty;
                    personDTO.MIddleName = string.Empty;
                    personDTO.Gender = string.Empty;
                    personDTO.Age = 0;
                    personDTO.Country = string.Empty;
                    personDTO.StateId = 0;
                    personDTO.state = string.Empty;
                    personDTO.DateOfBirth = DateTime.Now;
                    personDTO.City = string.Empty;
                    break;
                case "petDTO":
                    petDTO.Name = string.Empty;
                    petDTO.NickName = string.Empty;
                    petDTO.petType = string.Empty;
                    break;
                default:
                    break;
            }
        }
    }
}
