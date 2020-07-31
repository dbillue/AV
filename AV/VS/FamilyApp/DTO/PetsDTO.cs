using FamilyApp.Model;
using System.Collections.Generic;

namespace FamilyApp.DTO
{
    public class PetDTO
    {
        public string Name { get; set; }

        public string NickName { get; set; }

        public int PetTypeId { get; set; }

        public string petType = string.Empty;
        public List<PetTypes> petTypes;

        public PetDTO()
        {
            petType = string.Empty;
            petTypes = new List<PetTypes>();
        }
    }
}
