using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyApp.Model
{
    /// <summary>
    /// Database entity for PetTypes.
    /// </summary>
    [Table("PetTypes")]
    public class PetTypes
    {
        [Key]
        public int PetTypeId { get; set; }

        public string Type { get; set; }
    }
}
