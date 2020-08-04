using System;

namespace FamilyDemoAPIv2.Models
{
    public class AddPetDTO
    {
        public Guid PetId { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public int PetTypeId { get; set; }

        public Guid PersonId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
