using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyApp.Model
{
    /// <summary>
    /// Database entity for Pets.
    /// </summary>
    [Table("Pets")]
    public class Pet
    {
        [Key]
        public Guid PetId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string NickName { get; set; }

        [Required]
        public int PetTypeId { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public string petType = string.Empty;
        public string addPetType;
        public List<PetTypes> petTypes;
        public string addPetName = string.Empty;
        public string addPetNickName = string.Empty;

        public Pet()
        {
            petType = string.Empty;
            addPetType = string.Empty;
            petTypes = new List<PetTypes>();
            addPetName = string.Empty;
            addPetNickName = string.Empty;
        }
    }
}
