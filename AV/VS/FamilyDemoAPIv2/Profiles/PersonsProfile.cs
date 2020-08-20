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

            // Get birth state mapping.
            CreateMap<Models.GetBirthStateDTO, Entities.BirthState>();
            CreateMap<Entities.BirthState, Models.GetBirthStateDTO>();

            // Add pet mapping.
            CreateMap<Models.PetDTO, Entities.Pet>();
            CreateMap<Entities.Pet, Models.PetDTO>();

            // Get pets mapping.
            CreateMap<Entities.Pet, Models.PetDTO>();
            CreateMap<Models.PetDTO, Entities.Pet>();

            // Get pet type mapping.
            CreateMap<Models.GetPetTypesDTO, Entities.PetType>();
            CreateMap<Entities.PetType, Models.GetPetTypesDTO>();
        }
    }
}
