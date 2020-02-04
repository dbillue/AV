using System;
using FamilyDemoAPIv2.Models;
using AutoMapper;

namespace FamilyDemoAPIv2.Profiles
{
    public class PersonsProfile : Profile
    {
        public PersonsProfile()
        {
            // Add new person mapping.
            CreateMap<Models.AddPersonDTO, Entities.Person>();
            CreateMap<Entities.Person, Models.AddPersonDTO>();

            // Update person mapping.
            CreateMap<Models.UpdatePersonDTO, Entities.Person>();
            CreateMap<Entities.Person, Models.UpdatePersonDTO>();

            // Get person mapping.
            CreateMap<Entities.Person, Models.GetPersonDTO>();
        }
    }
}
