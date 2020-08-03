using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyDemoAPIv2.Entities
{
    /// <summary>
    /// Database entity for PetTypes.
    /// </summary>
    [Table("PetTypes")]
    public class PetType
    {
        [Key]
        public Int32 PetTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
    }
}
